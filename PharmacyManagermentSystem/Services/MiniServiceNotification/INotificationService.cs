using PharmacyManagermentSystem.Response;

namespace PharmacyManagermentSystem.Services.MiniServiceNotification
{
    public interface INotificationService
    {
        Task<List<NotificationResponse>> GetNotificationsAsync(string userId);
        Task<List<NotificationResponse>> GetAllNotificationsAsync(string userId);
        Task<bool> Read(int id);
    }
}
