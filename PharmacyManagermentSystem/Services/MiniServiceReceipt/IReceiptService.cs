using PharmacyManagermentSystem.Model;
using PharmacyManagermentSystem.Request;
using PharmacyManagermentSystem.Response;

namespace PharmacyManagermentSystem.Services.MiniServiceReceipt
{
    public interface IReceiptService
    {
        Task<ReceiptResponse> CreateReceipt(CreateReceiptRequest request);
        Task<ReceiptResponse> UpdateReceipt(int id, UpdateReceiptRequest request);
        Task<PaginatedList<ReceiptResponse>> GetAll(int pageIndex, int pageSize, int pharmacyId);
        Task<bool> Delete(int id);
        Task<bool> AddReceipt(ReceiptRequest request);
        Task<ReceiptDetailResponseById> getById(int id);
    }
}
