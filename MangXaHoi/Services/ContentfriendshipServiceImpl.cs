using MangXaHoi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MangXaHoi.Services
{
    public class ContentfriendshipServiceImpl : ContentfriendshipService
    {
        private DatabaseContext db;
        private IConfiguration configuration;
        public ContentfriendshipServiceImpl(DatabaseContext _db, IConfiguration _configuration)
        {
            this.db = _db;
            this.configuration = _configuration;
        }
        public List<dynamic> findByContentId(int contentid)
        {
            return db.Contents
           .Where(c => c.Id == contentid)
           .Select(c => new
           {
               Id = c.Id,
               Content1 = c.Content1,
               Date = c.Date,
               ContentPhoto = configuration["BaseUrl"] + "images/" + c.ContentPhoto,
               Fullname = c.User.Fullname,
               Photo = configuration["BaseUrl"] + "images/" + c.User.Photo,
               UserId = c.Userid,
               CountLike = c.Likes.Where(p => p.Contentid == c.Id).Count(),
               CountSave = c.Saves.Where(p => p.Contentid == c.Id).Count()
           })
           .ToList<dynamic>();
        }
        public bool Create(Content content)
        {
            if (content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    content.Date = DateTime.Now;
                    db.Contents.Add(content);

                    if (db.SaveChanges() > 0)
                    {
                        Notification notification = new Notification
                        {
                            Contentid = content.Id,
                            Status = false,
                            Userid = content.Userid
                        };

               

                        db.Notifications.Add(notification);
                 

                        if (db.SaveChanges() > 0)
                        {
                            transaction.Commit();
                            return true;
                        }
                        else
                        {
                            transaction.Rollback();
                            return false;
                        }
                    }
                    else
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }
        public List<dynamic> findByUserid1(int userid)
        {
            return db.Contents
             .Where(c => c.Userid == userid)
             .Select(c => new
             {
                 Id = c.Id,
                 Content1 = c.Content1,
                 Date = c.Date,
                 ContentPhoto = configuration["BaseUrl"] + "images/" + c.ContentPhoto,
                 Fullname = c.User.Fullname,
                 Photo = configuration["BaseUrl"] + "images/" + c.User.Photo,
                 UserId = c.Userid,
                 CountLike = c.Likes.Where(p => p.Contentid == c.Id).Count(),
                 CountSave = c.Saves.Where(p => p.Contentid == c.Id).Count()
             })
             .ToList<dynamic>();
        }
        public List<dynamic> findByUserid(int userid)
        {
            var userContent = db.Contents
                .Where(c => c.Userid == userid)
                .Select(c => new
                {
                    Id = c.Id,
                    Content1 = c.Content1,
                    Date = c.Date,
                    ContentPhoto = configuration["BaseUrl"] + "images/" + c.ContentPhoto,
                    Fullname = c.User.Fullname,
                    Photo = configuration["BaseUrl"] + "images/" + c.User.Photo,
                    UserId = c.User.Id,
                    StoryId = c.User.Stories.FirstOrDefault() != null ? c.User.Stories.FirstOrDefault().Id : (int?)null,
                    Status = c.User.Stories.FirstOrDefault() != null ? c.User.Stories.FirstOrDefault().Status : (bool?)null,
                    CountLike = c.Likes.Where(p => p.Contentid == c.Id).Count(),
                    CountSave = c.Saves.Where(p => p.Contentid == c.Id).Count()
                });

            var friendsContent1 = db.Contents
                .Join(db.Friendships,
                      c => c.Userid,
                      f => f.Userid1,
                      (c, f) => new { Content = c, Friendship = f })
                .Where(cf => cf.Friendship.Userid2 == userid)
                .Select(cf => new
                {
                    Id = cf.Content.Id,
                    Content1 = cf.Content.Content1,
                    Date = cf.Content.Date,
                    ContentPhoto = configuration["BaseUrl"] + "images/" + cf.Content.ContentPhoto,
                    Fullname = cf.Content.User.Fullname,
                    Photo = configuration["BaseUrl"] + "images/" + cf.Content.User.Photo,
                    UserId = cf.Content.User.Id,
                    StoryId = cf.Content.User.Stories.FirstOrDefault() != null ? cf.Content.User.Stories.FirstOrDefault().Id : (int?)null,
                    Status = cf.Content.User.Stories.FirstOrDefault() != null ? cf.Content.User.Stories.FirstOrDefault().Status : (bool?)null,
                    CountLike = cf.Content.Likes.Where(p => p.Contentid == cf.Content.Id).Count(),
                    CountSave = cf.Content.Saves.Where(p => p.Contentid == cf.Content.Id).Count()
                });

            var friendsContent2 = db.Contents
                .Join(db.Friendships,
                      c => c.Userid,
                      f => f.Userid2,
                      (c, f) => new { Content = c, Friendship = f })
                .Where(cf => cf.Friendship.Userid1 == userid)
                .Select(cf => new
                {
                    Id = cf.Content.Id,
                    Content1 = cf.Content.Content1,
                    Date = cf.Content.Date,
                    ContentPhoto = configuration["BaseUrl"] + "images/" + cf.Content.ContentPhoto,
                    Fullname = cf.Content.User.Fullname,
                    Photo = configuration["BaseUrl"] + "images/" + cf.Content.User.Photo,
                    UserId = cf.Content.User.Id,
                    StoryId = cf.Content.User.Stories.FirstOrDefault() != null ? cf.Content.User.Stories.FirstOrDefault().Id : (int?)null,
                    Status = cf.Content.User.Stories.FirstOrDefault() != null ? cf.Content.User.Stories.FirstOrDefault().Status : (bool?)null,
                    CountLike = cf.Content.Likes.Where(p => p.Contentid == cf.Content.Id).Count(),
                    CountSave = cf.Content.Saves.Where(p => p.Contentid == cf.Content.Id).Count()
                });

            var combinedContent = userContent.ToList()
                .Concat(friendsContent1.ToList())
                .Concat(friendsContent2.ToList());
            return combinedContent
                .OrderByDescending(c => c.Date)
                .Cast<dynamic>()
                .ToList();
        }
        public bool Remove(int id)
        {
            try
            {
                // Remove notifications with the given contentid
                var notifications = db.Notifications.Where(n => n.Contentid == id).ToList();
                foreach (var notification in notifications)
                {
                    db.Notifications.Remove(notification);
                }

                // Remove likes with the given contentid
                var likes = db.Likes.Where(n => n.Contentid == id).ToList();
                foreach (var like in likes)
                {
                    db.Likes.Remove(like);
                }

                // Remove comments with the given contentid
                var comments = db.Comments.Where(n => n.Contentid == id).ToList();
                foreach (var comment in comments)
                {
                    db.Comments.Remove(comment);
                }

                // Remove saves with the given contentid
                var saves = db.Saves.Where(n => n.Contentid == id).ToList();
                foreach (var save in saves)
                {
                    db.Saves.Remove(save);
                }

                // Save changes to the database after removing related records
                int changes = db.SaveChanges();
                if (changes <= 0)
                {
                    // Log if no changes were made
                    Console.WriteLine("No changes were made after deleting related records.");
                    return false;
                }

                // Find the friendship by id
                var content = db.Contents.Find(id);
                if (content == null)
                {
                    // Log that the friendship with the given id was not found
                    Console.WriteLine($"Friendship with id {id} not found.");
                    return false;
                }

                // Remove the friendship
                db.Contents.Remove(content);

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
                // For example: logger.LogError(e, "An error occurred while deleting the friendship and related records.");
                return false;
            }
        }


    }
}
    
