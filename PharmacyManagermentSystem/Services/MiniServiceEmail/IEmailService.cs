namespace PharmacyManagermentSystem.Services.MiniServiceEmail
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string htmlMessage);
    }
}
