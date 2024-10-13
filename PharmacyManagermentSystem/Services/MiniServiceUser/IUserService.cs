using PharmacyManagermentSystem.DTO;
using PharmacyManagermentSystem.Request;
using PharmacyManagermentSystem.Response;

namespace PharmacyManagermentSystem.Services.MiniServiceUser
{
    public interface IUserService
    {
        Task<ListUserResponse> GetUsers();
        Task<UserDTO> CreateUserAsync(CreateUserRequest user);
        Task<UserDTO> UpdateUserAsync(UpdateUserRequest user);
        Task DeleteUserAsync(string id);
    }
}
