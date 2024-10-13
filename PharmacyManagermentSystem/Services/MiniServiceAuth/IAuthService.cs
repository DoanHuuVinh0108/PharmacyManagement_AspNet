using PharmacyManagermentSystem.Request;
using PharmacyManagermentSystem.Response;

namespace PharmacyManagermentSystem.Services.MiniServiceAuth
{
    public interface IAuthService
    {
        Task<JwtResponse?> Login(SignInRequest request);
    }
}
