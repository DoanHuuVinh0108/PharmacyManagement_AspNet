using PharmacyManagermentSystem.Model;
using PharmacyManagermentSystem.Request;
using PharmacyManagermentSystem.Response;

namespace PharmacyManagermentSystem.Services.MiniServiceShift
{
    public interface IShiftService
    {
        Task<ShiftResponse> CreateShift(CreateShiftRequest request);
        Task<ShiftResponse> UpdateShift(UpdateShiftRequest request);
        Task<List<Shift>> GetAll();
        Task<bool> DeleteShift(DeleteShiftRequest request);
    }
}
