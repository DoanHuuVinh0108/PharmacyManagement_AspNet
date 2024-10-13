using PharmacyManagermentSystem.Model;
using PharmacyManagermentSystem.Request;
using PharmacyManagermentSystem.Response;

namespace PharmacyManagermentSystem.Services.MiniServiceSupplier
{
    public interface ISupplierService
    {
        Task<SupplierResponse> CreateSupplier(CreateSupplierRequest request);
        Task<SupplierResponse> UpdateSupplier(int id, UpdateSupplierRequest request);
        Task<List<Supplier>> GetAll();
        Task<bool> DeleteSupplier(int id);
    }
}
