using PharmacyManagermentSystem.Model;
using PharmacyManagermentSystem.Request;
using PharmacyManagermentSystem.Response;

namespace PharmacyManagermentSystem.Services.MiniServicePrescription
{
    public interface IPrescriptionService
    {
        Task<PrescriptionResponse> CreatePrescription(CreatePrescriptionRequest request);
        Task<bool> DeletePrescription(string id);
        Task<List<Prescription>> GetAll();
        Task<PrescriptionResponse> UpdatePrescription(UpdatePrescriptionRequest request, string id);
        Task<PaginatedList<PrescriptionResponse>> GetByPage(int pageIndex, int pageSize);
        Task<PrescriptionByIdResponse> GetById(string id);
    }
}
