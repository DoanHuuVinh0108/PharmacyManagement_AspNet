using PharmacyManagermentSystem.Model;
using PharmacyManagermentSystem.Request;
using PharmacyManagermentSystem.Response;

namespace PharmacyManagermentSystem.Services.MiniServiceUserShift
{
    public interface IUserShiftService
    {
        Task<UserShiftResponse> CreateUserShift(CreateUserShiftRequest request);
        Task<UserShiftResponse> UpdateUserShift(UpdateUserShiftRequest request);
        Task<List<UserShift>> GetAll();
        Task<bool> DeleteUserShift(DeleteUserShiftRequest request);
    }
}
