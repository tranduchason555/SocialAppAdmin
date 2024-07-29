using MangXaHoi.Models;

namespace MangXaHoi.Services
{
    public interface MessageService 
    {
        public List<dynamic> findByUserId(int userId);
        public bool Create(Message message);
        public List<dynamic> findbyfullname(int userId, string fullname);
        public List<dynamic> findbyfullname1( string fullname);
        public bool Delete(int id);
    }
}
