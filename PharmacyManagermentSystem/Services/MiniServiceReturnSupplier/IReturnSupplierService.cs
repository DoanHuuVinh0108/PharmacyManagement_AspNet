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
        Task<PaginatedList<ReturnSupplierResponse>> GetAll(int pageIndex, int pageSize, int pharmacyId);
        Task<bool> Delete(DeleteReturnSupplierRequest request);
    }
}
