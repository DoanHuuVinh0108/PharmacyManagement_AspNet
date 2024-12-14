using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using PharmacyManagermentSystem.DbContext;
using PharmacyManagermentSystem.Model;
using PharmacyManagermentSystem.Models;

public class TaskService : BackgroundService
{
    private readonly ILogger<TaskService> _logger;
    private readonly IServiceProvider _serviceProvider;

    public TaskService(ILogger<TaskService> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await DoWorkAsync();
            await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
        }
    }

    private async Task DoWorkAsync()
    {
        try
        {
            using var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

            DateOnly today = DateOnly.FromDateTime(DateTime.UtcNow);

            // Get user role Admin
            var admins = await userManager.GetUsersInRoleAsync("Admin");

            // Find medicines that will expire in 3 days
            var notifications = dbContext.Medicines
                .Where(x => x.ExpiryDate >= today && x.ExpiryDate <= today.AddDays(3))
                .GroupBy(group => new { group.CategoryId, group.BatchNumber, group.ExpiryDate, group.PharmacyId })
                .Select(group => new
                {
                    CategoryId = group.Key.CategoryId,
                    BatchNumber = group.Key.BatchNumber,
                    ExpiryDate = group.Key.ExpiryDate,
                    PharmacyId = group.Key.PharmacyId,
                    MedicineIds = group.Select(m => m.Id).ToList()
                })
                .ToList();

            List<Notification> notificationList = new List<Notification>();

            // Add admin notifications
            foreach (var notification in notifications)
            {
                _logger.LogInformation($"Medicine {notification.CategoryId} is expiring soon on {notification.ExpiryDate}.");

                notificationList.AddRange(admins.Select(admin => new Notification
                {
                    Title = "Thông báo thuốc sắp hết hạn sử dụng",
                    Content = $"Thuốc có số lô {notification.BatchNumber} và có mã danh mục là {notification.CategoryId} sẽ hết hạn vào ngày {notification.ExpiryDate} với các mã thuốc là: {string.Join(", ", notification.MedicineIds)}.",
                    Isread = 0,
                    CreatedAt = DateTime.UtcNow,
                    UserId = admin.Id
                }));
            }

            // Load pharmacy user mappings
            var pharmacyUserMappings = dbContext.Pharmacies
                .Select(p => new
                {
                    p.Id,
                    UserIds = p.Users.Select(u => u.Id).ToList()
                })
                .ToDictionary(p => p.Id, p => p.UserIds);

            // Add pharmacy user notifications
            foreach (var notification in notifications)
            {
                if (pharmacyUserMappings.TryGetValue(notification.PharmacyId, out var userIds))
                {
                    notificationList.AddRange(userIds.Select(userId => new Notification
                    {
                        Title = "Thông báo thuốc sắp hết hạn sử dụng",
                        Content = $"Thuốc có số lô {notification.BatchNumber} và có mã danh mục là {notification.CategoryId} sẽ hết hạn vào ngày {notification.ExpiryDate} với các mã thuốc là: {string.Join(", ", notification.MedicineIds)}.",
                        Isread = 0,
                        CreatedAt = DateTime.UtcNow,
                        UserId = userId
                    }));
                }
                else
                {
                    _logger.LogWarning($"No users found for PharmacyId: {notification.PharmacyId}.");
                }
            }

            // Add notifications to the database
            dbContext.Notifications.AddRange(notificationList);
            await dbContext.SaveChangesAsync();

            _logger.LogInformation("Notifications have been successfully added.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in task service");
        }
    }
}