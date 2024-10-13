using PharmacyManagermentSystem.Model;
using PharmacyManagermentSystem.Request;
using PharmacyManagermentSystem.Response;
using System.Threading.Tasks;

namespace PharmacyManagermentSystem.Services.MiniServiceReturnSupplier
{
    public interface IReturnSupplierService
    {
        Task<ReturnSupplierResponse> CreateReturnSupplier(CreateReturnSupplierRequest request);
        Task<ReturnSupplierResponse> UpdateReturnSupplier(UpdateReturnSupplierRequest request);
        Task<List<ReturnSupplier>> GetAll();
        Task<bool> Delete(DeleteReturnSupplierRequest request);
    }
}
