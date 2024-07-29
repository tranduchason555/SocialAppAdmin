using MangXaHoi.Models;

namespace MangXaHoi.Services
{
    public class SaveServiceImpl : SaveService
    {
        private DatabaseContext db;
        private IConfiguration configuration;
        public SaveServiceImpl(DatabaseContext _db, IConfiguration _configuration)
        {
            this.db = _db;
            this.configuration = _configuration;
        }
        public List<dynamic> findByUserId(int userid)
        {
            return db.Saves
                  .Where(p => p.Userid == userid)
                  .Select(p => new
                  {
                      Id = p.Id,
                      ContentId=p.Content.Id,
                      ContentPhoto= configuration["BaseUrl"] + "images/"+p.Content.ContentPhoto,
                      UserPhoto= configuration["BaseUrl"] + "images/"+p.User.Photo,
                      Fullname=p.User.Fullname,
                      UserId=p.Userid,
                      Date=p.Content.Date,
       
                  })
                  .ToList<dynamic>();
        }
        public bool Create(Safe save)
        {
            try
            {
                save.Date = DateTime.Now;
                db.Saves.Add(save);
                return db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool Delete(int id)
        {
            try
            {
                db.Saves.Remove(db.Saves.Find(id));
                return db.SaveChanges() > 0;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public List<dynamic> findByContentid(int contentid)
        {
            return db.Saves
                .Where(p => p.Contentid == contentid)
                .Select(p => new
                {
                    Id = p.Id,
                    ContentId = p.Content.Id,
                    ContentPhoto = configuration["BaseUrl"] + "images/" + p.Content.ContentPhoto,
                    UserPhoto = configuration["BaseUrl"] + "images/" + p.User.Photo,
                    Fullname = p.User.Fullname,
                    UserId = p.Userid,
                    Date = p.Content.Date,
                })
                .ToList<dynamic>();
        }

    }
}
