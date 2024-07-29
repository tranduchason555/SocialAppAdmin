using MangXaHoi.Models;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace MangXaHoi.Services
{
    public class UserServiceImpl:UserService
    {
        private DatabaseContext db;
        private IConfiguration configuration;
        public UserServiceImpl(DatabaseContext _db, IConfiguration _configuration)
        {
            this.db = _db;
            this.configuration = _configuration;
        }

        public bool Create(User user)
        {
            try
            {
                user.Roleid = 2;
                db.Users.Add(user);
                return db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool CreateAdmin(User user)
        {
            try
            {
                user.Roleid = 1;
                db.Users.Add(user);
                return db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public List<dynamic> findAll(int userid)
        {
            return db.Users.Where(p=>p.Roleid==2 && p.Id != userid)
                   .Select(p => new
                   {
                       Id = p.Id,
                       Email = p.Email,
                       Password = p.Password,
                       Fullname = p.Fullname,
                       Address = p.Address,
                       Photo = configuration["BaseUrl"] + "images/" + p.Photo,
                       Phone = p.Phone,
                       Age = p.Age,
                 /*      CountContent = p.Contents.Count(),
                       CountFriend = p.FriendshipUserid1Navigations.Count(),
                       Content = p.Contents.Select(c => c.Content1).ToList(),
                       ContentPhoto = p.Contents.Select(c => configuration["BaseUrl"] + "images/" + c.ContentPhoto).ToList(),*/
                       //Comments = p.Comments.Select(c => c.Content).ToList(),
                    /*   CountLike = p.Likes.Where(p => p.Userid == p.Id).Count(),*/
                       RoleId = p.Roleid
                   })
                   .ToList<dynamic>();
        }
        public List<dynamic> findAllUser()
        {
             return db.Users.Where(p => p.Roleid == 2)
                   .Select(p => new
                   {
                       Id = p.Id,
                       Email = p.Email,
                       Password = p.Password,
                       Fullname = p.Fullname,
                       Address = p.Address,
                       Photo = configuration["BaseUrl"] + "images/" + p.Photo,
                       Phone = p.Phone,
                       Age = p.Age,
                       /*      CountContent = p.Contents.Count(),
                             CountFriend = p.FriendshipUserid1Navigations.Count(),
                             Content = p.Contents.Select(c => c.Content1).ToList(),
                             ContentPhoto = p.Contents.Select(c => configuration["BaseUrl"] + "images/" + c.ContentPhoto).ToList(),*/
                       //Comments = p.Comments.Select(c => c.Content).ToList(),
                       /*   CountLike = p.Likes.Where(p => p.Userid == p.Id).Count(),*/
                       RoleId = p.Roleid
                   })
                   .ToList<dynamic>();
        }
        public List<dynamic> findAllAdmin()
        {
            return db.Users.Where(p => p.Roleid == 1)
                   .Select(p => new
                   {
                       Id = p.Id,
                       Email = p.Email,
                       Password = p.Password,
                       Fullname = p.Fullname,
                       Address = p.Address,
                       Photo = configuration["BaseUrl"] + "images/" + p.Photo,
                       Phone = p.Phone,
                       Age = p.Age,
                       /*      CountContent = p.Contents.Count(),
                             CountFriend = p.FriendshipUserid1Navigations.Count(),
                             Content = p.Contents.Select(c => c.Content1).ToList(),
                             ContentPhoto = p.Contents.Select(c => configuration["BaseUrl"] + "images/" + c.ContentPhoto).ToList(),*/
                       //Comments = p.Comments.Select(c => c.Content).ToList(),
                       /*   CountLike = p.Likes.Where(p => p.Userid == p.Id).Count(),*/
                       RoleId = p.Roleid
                   })
                   .ToList<dynamic>();
        }
        public dynamic findByEmail(string email)
        {
            return db.Users
      .Where(p => p.Email == email)
      .Select(p => new
      {
          Id = p.Id,
          Email = p.Email,
          Password = p.Password,
          Fullname = p.Fullname,
          Address = p.Address,
          Photo = configuration["BaseUrl"] + "images/" + p.Photo,
          Phone = p.Phone,
          Age = p.Age,
          CountContent = p.Contents.Count(),
          CountFriend = (p.FriendshipUserid1Navigations.FirstOrDefault() != null && p.FriendshipUserid1Navigations.FirstOrDefault().Userid1 == p.Id) ?
                        p.FriendshipUserid1Navigations.Count() :
                        (p.FriendshipUserid2Navigations.FirstOrDefault() != null ? p.FriendshipUserid2Navigations.Count() : 0),
          Content = p.Contents.Select(c => c.Content1).ToList(),
          ContentPhoto = p.Contents.Select(c => configuration["BaseUrl"] + "images/" + c.ContentPhoto).ToList(),
          ContentId = p.Contents.FirstOrDefault() != null ? p.Contents.FirstOrDefault().Id : 0,
          CountLike = (p.Likes != null ? p.Likes.Where(p => p.Contentid == p.Content.Id && p.Userid!=p.Id).Count() : 0),
          CountNotication = p.Notifications.Where(k => (k.Userid != p.FriendshipUserid1Navigations.First().Userid1) || (k.Userid != p.FriendshipUserid2Navigations.First().Userid1)).Count(),
          RoleId = p.Roleid
          // StatusFriend=p.
      })
      .FirstOrDefault();
        }
        public dynamic findByEmail1(string email, int userid)
        {
            return db.Users
      .Where(p => p.Email == email )
      .Select(p => new
      {
          Id = p.Id,
          Email = p.Email,
          Password = p.Password,
          Fullname = p.Fullname,
          Address = p.Address,
          Photo = configuration["BaseUrl"] + "images/" + p.Photo,
          Phone = p.Phone,
          Age = p.Age,
          CountContent = p.Contents.Count(),
          CountFriend = (p.FriendshipUserid1Navigations.FirstOrDefault() != null && p.FriendshipUserid1Navigations.FirstOrDefault().Userid1 == p.Id) ?
                        p.FriendshipUserid1Navigations.Count() :
                        (p.FriendshipUserid2Navigations.FirstOrDefault() != null ? p.FriendshipUserid2Navigations.Count() : 0),
          Content = p.Contents.Select(c => c.Content1).ToList(),
          ContentPhoto = p.Contents.Select(c => configuration["BaseUrl"] + "images/" + c.ContentPhoto).ToList(),
          ContentId = p.Contents.FirstOrDefault() != null ? p.Contents.FirstOrDefault().Id : 0,
          CountLike = (p.Likes != null ? p.Likes.Where(p => p.Contentid == p.Content.Id && p.Userid != p.Id).Count() : 0),
          CountNotication = p.Notifications.Where(k => (k.Userid != p.FriendshipUserid1Navigations.First().Userid1) || (k.Userid != p.FriendshipUserid2Navigations.First().Userid1)).Count(),
          Status = db.Friendships
       .Where(k => (k.Userid1 == p.Id || k.Userid2 == p.Id) && (k.Userid2 == userid || k.Userid1 == userid))
       .Select(k => k.Status)
       .FirstOrDefault(),
          RoleId = p.Roleid,
          FriendshipId = db.Friendships
       .Where(k => (k.Userid1 == userid || k.Userid2 == userid))
       .Select(k => k.Id)
       .FirstOrDefault(),
      })
      .FirstOrDefault();
        }
        public List<dynamic> findByFullname(string fullname)
        {
            return db.Users
                     .Where(p => p.Fullname.Contains(fullname))
                     .Select(p => new
                     {
                         p.Id,
                         p.Email,
                         p.Password,
                         p.Fullname,
                         p.Address,
                         Photo = configuration["BaseUrl"] + "images/" + p.Photo,
                         p.Phone,
                         p.Age,
                         CountContent = p.Contents.Count(),
                         CountFriend = p.FriendshipUserid1Navigations.Count(),
                         Content = p.Contents.Select(c => c.Content1).ToList(),
              
                         ContentPhoto = p.Contents.Select(c => configuration["BaseUrl"] + "images/" + c.ContentPhoto).ToList(),
                         //Comments = p.Comments.Select(c => c.Content).ToList(),
                         CountLike = p.Likes.Where(p => p.Contentid == p.Content.Id && p.Userid != p.Id).Count(),
                         RoleId = p.Roleid
                     })
                     .ToList<dynamic>(); // Convert to List<dynamic>
        }

        public dynamic findByUserid(int userid)
        {

            return db.Users.Where(p => p.Id == userid).Select(p => new
            {
                Id = p.Id,
                Email=p.Email,
                Password=p.Password,
                Fullname=p.Fullname,
                Address=p.Address,
                Photo= configuration["BaseUrl"] + "images/" + p.Photo,
                Phone=p.Phone,
                Age=p.Age,
                CountContent = p.Contents.Count(),
                CountFriend = p.FriendshipUserid1Navigations.Count(),
                Content = p.Contents.Select(c => c.Content1).ToList(),
                ContentPhoto = p.Contents.Select(c => configuration["BaseUrl"] + "images/" + c.ContentPhoto).ToList(),
                CountLike=p.Likes.Where(p=> p.Contentid == p.Content.Id && p.Userid != p.Id).Count(),
                RoleId = p.Roleid
                //  Comments = p.Comments.Select(c => c.Content).ToList(),
            }).FirstOrDefault();
        }

        public bool login(string email, string password)
        {
            return db.Users.Count(a => a.Email == email && a.Password == password) > 0;
        }

        public bool Login(string email, string password)
        {
            var account = db.Users.SingleOrDefault(x => x.Email == email);
            if (account != null && BCrypt.Net.BCrypt.Verify(password, account.Password))
            {
                return true; // Mật khẩu đúng
            }
            return false; // Mật khẩu sai
        }

        public List<dynamic> findAllAdminUser()
        {
            return db.Users
                     .OrderByDescending(p => p.Roleid == 1)  // Sắp xếp sao cho Roleid == 1 xuất hiện trước
                     .Select(p => new
                     {
                         Id = p.Id,
                         Email = p.Email,
                         Password = p.Password,
                         Fullname = p.Fullname,
                         Address = p.Address,
                         Photo = configuration["BaseUrl"] + "images/" + p.Photo,
                         Phone = p.Phone,
                         Age = p.Age,
                         // Các thuộc tính khác nếu cần
                         RoleId = p.Roleid
                     })
                     .ToList<dynamic>();  // Chuyển đổi kết quả thành danh sách
        }

        public bool Update(User user)
        {
            try
            {
                db.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                return db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool Remove(int id)
        {
            try
            {
                db.Users.Remove(db.Users.Find(id));
                return db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

   
    }
}
