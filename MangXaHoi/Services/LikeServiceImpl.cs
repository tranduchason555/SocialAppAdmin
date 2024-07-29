using MangXaHoi.Models;

namespace MangXaHoi.Services
{
    public class LikeServiceImpl : LikeService
    {
        private DatabaseContext db;
        private IConfiguration configuration;
        public LikeServiceImpl(DatabaseContext _db, IConfiguration _configuration)
        {
            this.db = _db;
            this.configuration = _configuration;
        }

        public bool Create(Like like)
        {
            try
            {
                like.Date = DateTime.Now;
                db.Likes.Add(like);
                return db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<dynamic> findByContentid(int contentid)
        {
            return db.Likes
                     .Where(p => p.Contentid == contentid)
                     .Select(p => new
                     {
                         Id = p.Id,
                         ContentId = p.Contentid,
                         Userid = p.User.Id,
                     })
                     .ToList<dynamic>(); // Convert to List<dynamic>
        }

        public bool Delete(int id)
        {
            try
            {
                db.Likes.Remove(db.Likes.Find(id));
                return db.SaveChanges() > 0;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
