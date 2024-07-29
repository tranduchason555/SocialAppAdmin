using MangXaHoi.Models;

namespace MangXaHoi.Services
{
    public interface SaveService 
    {
        public List<dynamic> findByUserId(int userid);
        public bool Create(Safe save);
        public bool Delete(int id);
        public List<dynamic> findByContentid(int contentid);

    }
}
