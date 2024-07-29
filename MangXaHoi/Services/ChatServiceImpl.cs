using MangXaHoi.Models;

namespace MangXaHoi.Services
{
    public class ChatServiceImpl : ChatService
    {
        private DatabaseContext db;
        private IConfiguration configuration;
        public ChatServiceImpl(DatabaseContext _db, IConfiguration _configuration)
        {
            this.db = _db;
            this.configuration = _configuration;
        }
   
        public bool Create(Chat chat)
        {
            try
            {
                if (chat == null)
                {
                    throw new ArgumentNullException(nameof(chat));
                }

                chat.Date = DateTime.Now;
                db.Chats.Add(chat);

                /*if (db.SaveChanges() > 0)
                {
                    Notification notification = new Notification
                    {
                        Messageid =  chat.Messagesid,
                        Status = false,
                        Userid=chat.Userid,
                    };
                    db.Notifications.Add(notification);
                    return db.SaveChanges() > 0;
                }*/

                return db.SaveChanges() > 0; ;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
