using MangXaHoi.Models;

namespace MangXaHoi.Services
{
    public class FriendshipServiceImpl : FriendshipService
    {
        private DatabaseContext db;
        private IConfiguration configuration;
        public FriendshipServiceImpl(DatabaseContext _db, IConfiguration _configuration)
        {
            this.db = _db;
            this.configuration = _configuration;
        }
        public bool Create(Friendship friendShip)
        {
            try
            {
                if (friendShip == null)
                {
                    throw new ArgumentNullException(nameof(friendShip));
                }

                // Check if the friendship already exists
                var existingFriendship = db.Friendships.FirstOrDefault(
                    f => (f.Userid1 == friendShip.Userid1 && f.Userid2 == friendShip.Userid2)
                      || (f.Userid1 == friendShip.Userid2 && f.Userid2 == friendShip.Userid1));

                if (existingFriendship != null)
                {
                    Console.WriteLine("Friendship already exists in the database.");
                    return false; // Return false or handle as needed
                }

                // If not exists, create new friendship
                friendShip.Status = false;
                friendShip.Date = DateTime.Now;
                db.Friendships.Add(friendShip);

                // Save changes to database to get the friendship ID
                if (db.SaveChanges() > 0)
                {
                    // Create a notification
                    Notification notification = new Notification
                    {
                        Friendshipid = friendShip.Id, // Assuming 'Id' is set after saving
                        Status = false,
                        Userid = friendShip.Userid1
                    };
                    db.Notifications.Add(notification);

                    // Check if Useridgui and Useridnhan are not the same
                    if (friendShip.Userid1 != friendShip.Userid2)
                    {
                        // Check if a message already exists between these users
                        bool messageExists = db.Messages.Any(m =>
                            (m.Useridgui == friendShip.Userid1 && m.Useridnhan == friendShip.Userid2) ||
                            (m.Useridgui == friendShip.Userid2 && m.Useridnhan == friendShip.Userid1)
                        );

                        if (!messageExists)
                        {
                            // Create a new message
                            Message message = new Message
                            {
                                Useridgui = friendShip.Userid1,
                                Useridnhan = friendShip.Userid2,
                                Date = DateTime.Now,
                                ContentPhoto = "noimage.jpg"
                            };
                            db.Messages.Add(message);
                        }

                        // Save notification and message
                        return db.SaveChanges() > 0;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool Update(Friendship friendShip)
        {
            try
            {
                friendShip.Date=DateTime.Now;
                db.Entry(friendShip).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                return db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<dynamic> findByUserId(int userid)
        {
            return db.Friendships
                     .Where(p => p.Userid1 == userid || p.Userid2 == userid)
                     .Select(p => new
                     {
                         p.Id,
                         Content1 = p.Userid1Navigation.Contents.Select(x => x.Content1).ToList(),
                         ContentPhoto = p.Userid1Navigation.Contents
               .Select(x => configuration["BaseUrl"] + "images/" + x.ContentPhoto)
               .ToList(),
                         Date = p.Userid1Navigation.Contents.Select(x => x.Date).ToList(),
                         Content2 = p.Userid2Navigation.Contents.Select(x => x.Content1).ToList(),
                         ContentPhoto2 = p.Userid2Navigation.Contents
               .Select(x => configuration["BaseUrl"] + "images/" + x.ContentPhoto)
               .ToList(),
                         Date2 = p.Userid2Navigation.Contents.Select(x => x.Date).ToList(),
                     })
                     .ToList<dynamic>(); // Return as a list of dynamic objects
        }
        public dynamic FindByEmail(string email, int userid)
        {
   
            return db.Friendships
                .Where(p =>
                    (p.Userid1Navigation.Email == email && p.Userid2Navigation.Email != email) ||
                    (p.Userid2Navigation.Email == email && p.Userid1Navigation.Email != email)
                )
                .Select(p => new
                {
                    Id = p.Id,
                    Email = p.Userid1Navigation.Email == email ? p.Userid1Navigation.Email : p.Userid2Navigation.Email,
                    Status = db.Friendships
       .Where(k => (k.Userid1 == p.Id || k.Userid2 == p.Id) && (k.Userid2 == userid || k.Userid1 == userid))
       .Select(k => k.Status)
       .FirstOrDefault(),
                    Date = p.Date,
                    Password = p.Userid1Navigation.Password != null? p.Userid1Navigation.Password : p.Userid2Navigation.Password,
                    Fullname = p.Userid1Navigation.Fullname != null ? p.Userid1Navigation.Fullname : p.Userid2Navigation.Password,
      /*              Address = p.Address,
                    Photo = configuration["BaseUrl"] + "images/" + p.Photo,
                    Phone = p.Phone,
                    Age = p.Age,*/
                })
                .FirstOrDefault();
        }

        public List<dynamic> findByFriendship(int userid)
        {
            return db.Friendships
                .Where(p => (p.Userid1 == userid || p.Userid2 == userid) && p.Userid1 != p.Userid2)
                .Select(p => new
                {
                    Id = p.Id,
                    Date = p.Date,
                    Fullname = p.Userid1 == userid ? p.Userid2Navigation.Fullname : p.Userid1Navigation.Fullname,
                    Photo = p.Userid1 == userid ? p.Userid2Navigation.Photo : p.Userid1Navigation.Photo,
                })
                .ToList<dynamic>();
        }
        public dynamic findByFullnameship(string fullname, int userid)
        {
            var userContent = db.Friendships
                .Where(m => (m.Userid1 == userid && m.Userid2Navigation.Fullname.Contains(fullname)) ||
                            (m.Userid2 == userid && m.Userid1Navigation.Fullname.Contains(fullname)))
                .Select(p => new
                {
                    Id = p.Id,
                    Date = p.Date,
                    FriendUserId = p.Userid1 == userid ? p.Userid2 : p.Userid1,
                    Fullname = p.Userid1 == userid ? p.Userid2Navigation.Fullname : p.Userid1Navigation.Fullname,
                    Photo = p.Userid1 == userid ? p.Userid2Navigation.Photo : p.Userid1Navigation.Photo,
                })
                .GroupBy(p => p.FriendUserId)
                .Select(g => g.First())
                .ToList<dynamic>();

            return userContent;
        }





        public bool Remove(int id)
        {
            try
            {
                // Find the associated notification by FriendshipId
                var notification = db.Notifications.FirstOrDefault(n => n.Friendshipid == id);
                if (notification != null)
                {
                    // Remove the notification
                    db.Notifications.Remove(notification);

                    // Save changes to the database
                    int changes = db.SaveChanges();
                    if (changes <= 0)
                    {
                        // Log if no changes were made
                        Console.WriteLine("No changes were made after deleting the notification.");
                        return false;
                    }
                }
                else
                {
                    // Log that the notification with the given FriendshipId was not found
                    Console.WriteLine($"Notification with FriendshipId {id} not found.");
                }

                // Find the friendship by id
                var friendship = db.Friendships.Find(id);
                if (friendship == null)
                {
                    // Log that the friendship with the given id was not found
                    Console.WriteLine($"Friendship with id {id} not found.");
                    return false;
                }

                // Remove the friendship
                db.Friendships.Remove(friendship);

                // Save changes after removing the friendship
                int changesFriendship = db.SaveChanges();
                if (changesFriendship > 0)
                {
                    return true;
                }
                else
                {
                    // Log if no changes were made
                    Console.WriteLine("No changes were made after deleting the friendship.");
                    return false;
                }
            }
            catch (Exception e)
            {
                // Log the exception details
                Console.WriteLine($"An error occurred: {e.Message}");
                // For example: logger.LogError(e, "An error occurred while deleting the friendship and notification.");
                return false;
            }
        }


    }
}
