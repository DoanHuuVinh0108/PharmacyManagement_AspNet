using PharmacyManagermentSystem.Model;
using PharmacyManagermentSystem.Request;
using PharmacyManagermentSystem.Response;

namespace PharmacyManagermentSystem.Services.MiniServiceDestructiveMedicine
{
    public interface IDestructiveMedicineService
    {
        Task<DestructiveMedicineResponse> CreateDestructiveMedicine(CreateDestructiveMedicineRequest request);
        Task<DestructiveMedicineResponse> UpdateDestructiveMedicine(UpdateDestructiveMedicineRequest request);
        Task<List<DestructiveMedicine>> GetAll();
        Task<bool> DeleteDestructiveMedicine(DeleteDestructiveMedicineRequest request);
    }
}
