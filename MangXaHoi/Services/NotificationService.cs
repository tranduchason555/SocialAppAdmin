namespace MangXaHoi.Services
{
    public interface NotificationService
    {
        public List<dynamic> NotificationFriendshipId(int userid);
        public bool Remove(int id);
    }
}
