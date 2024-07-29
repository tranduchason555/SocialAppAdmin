using MangXaHoi.Models;

namespace MangXaHoi.Services
{
    public interface StoryfriendshipService 
    {
        public List<dynamic> findByUserid(int userid);
        public List<dynamic> findByUserid1(int userid);
        public bool Create(Story story);

    }
}
