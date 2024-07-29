using MangXaHoi.Models;

namespace MangXaHoi.Services
{
    public interface CommentService 
    {
      public List<dynamic> findByContentid(int contentid);
        public bool Create(Comment comment);
        public List<dynamic> findByMessageid(int messageid);

    }
}
