using MangXaHoi.Models;

namespace MangXaHoi.Services
{
    public interface ContentfriendshipService 
    {
      public List<dynamic> findByUserid(int userid);
        public bool Create(Content content);
        public List<dynamic> findByUserid1(int userid);

        public List<dynamic> findByContentId(int contentid);
        public bool Remove(int id);

    }
}
