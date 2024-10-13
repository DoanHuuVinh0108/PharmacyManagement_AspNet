using PharmacyManagermentSystem.Model;
using PharmacyManagermentSystem.Request;
using PharmacyManagermentSystem.Response;

namespace PharmacyManagermentSystem.Services.MiniServicePharmacy
{
    public interface IPharmacyService
    {
        Task<PharmacyResponse> CreatePharmacy(CreatePharmacyRequest payload);
        Task<PharmacyResponse> UpdatePharmacy(int id, UpdatePharmacyRequest payload);
        Task<List<Pharmacy>> GetAllPharmacy();
        Task<bool> DeletePharmacy(int id);
    }
}
