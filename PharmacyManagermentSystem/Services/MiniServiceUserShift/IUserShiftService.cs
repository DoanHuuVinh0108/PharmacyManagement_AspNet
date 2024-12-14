using PharmacyManagermentSystem.Model;
using PharmacyManagermentSystem.Request;
using PharmacyManagermentSystem.Response;

namespace PharmacyManagermentSystem.Services.MiniServiceUserShift
{
    public interface IUserShiftService
    {
        Task<UserShiftResponse> CreateUserShift(CreateUserShiftRequest request);
        Task<UserShiftResponse> UpdateUserShift(UpdateUserShiftRequest request);
        Task<List<UserShift>> GetAll(DateOnly from, DateOnly to, int pharmacyId);
        Task<bool> DeleteUserShift(DeleteUserShiftRequest request);
        Task<List<UserShift>> GetById(string id);
        Task<List<UserShiftByDateResponse>> GetByDate(DateOnly date, int pharmacyId);
    }
}
