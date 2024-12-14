using Microsoft.EntityFrameworkCore;
using PharmacyManagermentSystem.DbContext;
using PharmacyManagermentSystem.Response;

namespace PharmacyManagermentSystem.Services.MiniServiceNotification
{
    public class NotificationService : INotificationService
    {
        private readonly MyDbContext _dbContext;
        public NotificationService(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<NotificationResponse>> GetNotificationsAsync(string userId)
        {
            var result = await _dbContext.Notifications
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => new NotificationResponse
                {
                    Id = x.Id,
                    Title = x.Title,
                    Content = x.Content,
                    Isread = x.Isread,
                    CreatedAt = x.CreatedAt,
                    UserId = x.UserId
                })
                .Take(5)
                .ToListAsync(); // Execute the query asynchronously and return a List

            return result;
        }
        public async Task<List<NotificationResponse>> GetAllNotificationsAsync(string userId)
        {
            var result = await _dbContext.Notifications
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => new NotificationResponse
                {
                    Id = x.Id,
                    Title = x.Title,
                    Content = x.Content,
                    Isread = x.Isread,
                    CreatedAt = x.CreatedAt,
                    UserId = x.UserId
                })
                .ToListAsync(); // Execute the query asynchronously and return a List

            return result;
        }
        public async Task<bool> Read(int id)
        {
            var notification = await _dbContext.Notifications.FindAsync(id);
            if (notification == null)
            {
                return false;
            }
            notification.Isread = 1;
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
