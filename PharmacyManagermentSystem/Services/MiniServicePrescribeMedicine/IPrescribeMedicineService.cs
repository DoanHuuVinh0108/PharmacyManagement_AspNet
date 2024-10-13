using PharmacyManagermentSystem.Model;
using PharmacyManagermentSystem.Request;
using PharmacyManagermentSystem.Response;

namespace PharmacyManagermentSystem.Services.MiniServicePrescribeMedicine
{
    public interface IPrescribeMedicineService
    {
        Task<PrescribeMedicineResponse> CreatePrescribeMedicine(CreatePrescribeMedicineRequest request);
        Task<PrescribeMedicineResponse> UpdatePrescribeMedicine(UpdatePrescribeMedicineRequest request);
        Task<List<PrescribeMedicine>> GetAll();
        Task<PrescribeMedicineResponse> DeletePrescribeMedicine(DeletePrescribeMedicineRequest request);
    }
}
