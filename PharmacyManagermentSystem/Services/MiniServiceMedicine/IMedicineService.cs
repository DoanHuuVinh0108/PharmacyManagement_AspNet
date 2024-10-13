using PharmacyManagermentSystem.Model;
using PharmacyManagermentSystem.Request;
using PharmacyManagermentSystem.Response;

namespace PharmacyManagermentSystem.Services.MiniServiceMedicine
{
    public interface IMedicineService
    {
        Task<MedicineResponse> CreateMedicine(CreateMedicineRequest request);
        Task<MedicineResponse> UpdateMedicine(UpdateMedicineRequest request);
        Task<List<Medicine>> GetAll();
        Task<bool> DeleteMedicine(DeleteMedicineRequest request);
    }
}
