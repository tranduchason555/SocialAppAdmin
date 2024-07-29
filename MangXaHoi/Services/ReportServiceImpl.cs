using MangXaHoi.Models;

namespace MangXaHoi.Services
{
    public class ReportServiceImpl : ReportService
    {
        private DatabaseContext db;
        private IConfiguration configuration;
        public ReportServiceImpl(DatabaseContext _db, IConfiguration _configuration)
        {
            this.db = _db;
            this.configuration = _configuration;
        }

       

        public List<dynamic> findAll()
        {
            return db.Reports
                 .Select(p => new
                 {
                     Id = p.Id,
                     ContentId = p.Content.Id,
                     Content = p.Content.Content1,
                     Report = p.Contentreport,
                     ContentPhoto = configuration["BaseUrl"] + "images/" + p.Content.ContentPhoto,
                     UserPhoto = configuration["BaseUrl"] + "images/" + p.User.Photo,
                     Fullname = p.User.Fullname,
                     UserId = p.Userid,
                     Date = p.Date,

                 })
                 .ToList<dynamic>();
        }
        public bool Create(Report report)
        {
            try
            {
                report.Date = DateTime.Now;
                db.Reports.Add(report);
                return db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool Delete(int id)
        {
            try
            {
                // Find the report by id
                var report = db.Reports.Find(id);
                if (report == null)
                {
                    // Log that the report with the given id was not found
                    Console.WriteLine($"Report with id {id} not found.");
                    return false;
                }

                // Remove the report first
                db.Reports.Remove(report);

                // Save changes after removing the report
                int changes = db.SaveChanges();
                if (changes <= 0)
                {
                    // Log if no changes were made
                    Console.WriteLine("No changes were made after deleting the report.");
                    return false;
                }

                // Find the associated content by Contentid
                var content = db.Contents.Find(report.Contentid);
                if (content == null)
                {
                    // Log that the content with the given id was not found
                    Console.WriteLine($"Content with id {report.Contentid} not found.");
                    return false;
                }

                // Remove the content
                db.Contents.Remove(content);

                // Save changes to the database
                changes = db.SaveChanges();
                if (changes > 0)
                {
                    return true;
                }
                else
                {
                    // Log if no changes were made
                    Console.WriteLine("No changes were made after deleting the content.");
                    return false;
                }
            }
            catch (Exception e)
            {
                // Log the exception details
                Console.WriteLine($"An error occurred: {e.Message}");
                // For example: logger.LogError(e, "An error occurred while deleting the report and content.");
                return false;
            }
        }


    }
}
