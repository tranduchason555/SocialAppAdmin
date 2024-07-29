using MangXaHoi.Models;

namespace MangXaHoi.Services
{
    public interface ReportService 
    {
        public List<dynamic> findAll();
        public bool Delete(int id);
        public bool Create(Report report);

    }
}
