using PharmacyManagermentSystem.Model;
using PharmacyManagermentSystem.Request;
using PharmacyManagermentSystem.Response;

namespace PharmacyManagermentSystem.Services.MiniServiceOrderDetail
{
    public interface IOrderDetailService
    {
        Task<OrderDetailResponse> CreateOrderDetail(CreateOrderDetailRequest request);
        Task<OrderDetailResponse> UpdateOrderDetail(UpdateOrderDetailRequest request);
        Task<List<OrderDetail>> GetAll();
        Task<bool> DeleteOrderDetail(DeleteOrderDetailRequest request);
    }
}
