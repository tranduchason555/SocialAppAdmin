using MangXaHoi.Models;

namespace MangXaHoi.Services
{
    public interface UserService
    {
        public bool login(string email, string password);
        public bool Login(string email, string password);
        public dynamic findByUserid(int userid);
        public bool Create(User user);
        public List<dynamic> findAll(int userid);
        public dynamic findByEmail(string email);
        public List<dynamic> findByFullname(string fullname);
        public dynamic findByEmail1(string email,int userid);
        public List<dynamic> findAllAdmin();
        public List<dynamic> findAllUser();
        public List<dynamic> findAllAdminUser();
        public bool CreateAdmin(User user);
        public bool Update(User user);
        public bool Remove(int id);
    }
}
