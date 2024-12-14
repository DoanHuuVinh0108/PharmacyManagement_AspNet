using PharmacyManagermentSystem.Request;
using PharmacyManagermentSystem.Response;

namespace PharmacyManagermentSystem.Services.MiniServiceAuth
{
    public interface IAuthService
    {
        Task<JwtResponse?> Login(SignInRequest request);
        Task<bool> SendPasswordResetTokenAsync(string email);
        Task<bool> ResetPasswordAsync(string email, string token, string newPassword);
    }
}
