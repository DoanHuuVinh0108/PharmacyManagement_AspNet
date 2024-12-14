using PharmacyManagermentSystem.Model;
using PharmacyManagermentSystem.Request;
using PharmacyManagermentSystem.Response;

namespace PharmacyManagermentSystem.Services.MiniServiceDestructiveMedicine
{
    public interface IDestructiveMedicineService
    {
        Task<DestructiveMedicineResponse> CreateDestructiveMedicine(CreateDestructiveMedicineRequest request);
        Task<DestructiveMedicineResponse> UpdateDestructiveMedicine(UpdateDestructiveMedicineRequest request);
        Task<PaginatedList<DestructiveMedicineResponse>> GetAll(int pageIndex, int pageSize, int pharmacyId);
        Task<bool> DeleteDestructiveMedicine(DeleteDestructiveMedicineRequest request);
    }
}
