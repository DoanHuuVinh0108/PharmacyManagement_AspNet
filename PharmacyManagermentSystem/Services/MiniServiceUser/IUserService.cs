using PharmacyManagermentSystem.DTO;
using PharmacyManagermentSystem.Request;
using PharmacyManagermentSystem.Response;

namespace PharmacyManagermentSystem.Services.MiniServiceUser
{
    public interface IUserService
    {
        Task<PaginatedList<UserDTO>> GetUsers(int pageIndex, int pageSize);
        Task<UserDTO> CreateUserAsync(CreateUserRequest user);
        Task<UserDTO> UpdateUserAsync(UpdateUserRequest user);
        Task DeleteUserAsync(string id);
        Task<List<UserDTO>> FindByPhoneNumber(string phoneNumber);
        Task<List<UserDTO>> GetByRole(string role);
    }
}
