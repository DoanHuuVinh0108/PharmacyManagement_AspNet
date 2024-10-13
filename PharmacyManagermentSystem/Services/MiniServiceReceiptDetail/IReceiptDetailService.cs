using PharmacyManagermentSystem.Model;
using PharmacyManagermentSystem.Request;
using PharmacyManagermentSystem.Response;

namespace PharmacyManagermentSystem.Services.MiniServiceReceiptDetail
{
    public interface IReceiptDetailService
    {
        Task<ReceiptDetailResponse> CreateReceiptDetail(CreateReceiptDetailRequest request);
        Task<ReceiptDetailResponse> UpdateReceiptDetail(UpdateReceiptDetailRequest request);
        Task<List<ReceiptDetail>> GetAll();
        Task<bool> DeleteReceiptDetail(DeleteReceiptDetailRequest request);
    }
}
