using PharmacyManagermentSystem.Model;
using PharmacyManagermentSystem.Request;
using PharmacyManagermentSystem.Response;

namespace PharmacyManagermentSystem.Services.MiniServiceMedicine
{
    public interface IMedicineService
    {
        Task<MedicineResponse> CreateMedicine(CreateMedicineRequest request);
        Task<MedicineResponse> UpdateMedicine(UpdateMedicineRequest request);
        Task<PaginatedList<Medicine>> GetAll(int pageIndex, int pageSize,int pharmacyId);
        Task<bool> DeleteMedicine(DeleteMedicineRequest request);
        Task<List<string>> GetByCategoryId(string CategoryId,int pharmacyId);
        Task<List<string>> GetByBatchNumber(string BatchNumber, string CategoryId);
        Task<int> GetQuantity(string batchNumber, string categoryId, string medicineId);

    }
}
