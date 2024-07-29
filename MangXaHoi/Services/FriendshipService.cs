using MangXaHoi.Models;

namespace MangXaHoi.Services
{
    public interface FriendshipService
    {
        public List<dynamic> findByUserId(int userid);
        public bool Create(Friendship friendShip);
        public dynamic FindByEmail(string email, int userid);

        public bool Update(Friendship friendShip);

        public List<dynamic> findByFriendship(int userid);
        public dynamic findByFullnameship(string fullname,int userid);
        public bool Remove(int id);
    }
}
