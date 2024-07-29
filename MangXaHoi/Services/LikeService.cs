using MangXaHoi.Models;

namespace MangXaHoi.Services
{
    public interface LikeService 
    {
        public bool Create(Like like);
        public List<dynamic> findByContentid(int contentid);
        public bool Delete(int id);

    }
}
