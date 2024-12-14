using PharmacyManagermentSystem.Model;
using PharmacyManagermentSystem.Request;
using PharmacyManagermentSystem.Response;

namespace PharmacyManagermentSystem.Services.MiniServiceShift
{
    public interface IShiftService
    {
        Task<bool> CreateShift(CreateShiftRequest request);
        Task<ShiftResponse> UpdateShift(UpdateShiftRequest request);
        Task<List<Shift>> GetAll(DateOnly from, DateOnly to, int pharmacyId);
        Task<bool> DeleteShift(DeleteShiftRequest request);
        Task<PaginatedList<Shift>> getByPage(int pageIndex, int pageSize, int pharmacyId);
    }
}
