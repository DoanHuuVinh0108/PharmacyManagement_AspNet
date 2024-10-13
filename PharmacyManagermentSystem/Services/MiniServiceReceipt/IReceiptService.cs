using PharmacyManagermentSystem.Model;
using PharmacyManagermentSystem.Request;
using PharmacyManagermentSystem.Response;

namespace PharmacyManagermentSystem.Services.MiniServiceReceipt
{
    public interface IReceiptService
    {
        Task<ReceiptResponse> CreateReceipt(CreateReceiptRequest request);
        Task<ReceiptResponse> UpdateReceipt(int id, UpdateReceiptRequest request);
        Task<List<Receipt>> GetAll();
        Task<bool> Delete(int id);
    }
}
