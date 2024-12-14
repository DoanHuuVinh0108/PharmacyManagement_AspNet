using PharmacyManagermentSystem.Model;
using PharmacyManagermentSystem.Request;
using PharmacyManagermentSystem.Response;

namespace PharmacyManagermentSystem.Services.MiniServiceOrder
{
    public interface IOrderService
    {
        Task<OrderResponse> CreateOrder(CreateOrderRequest request);
        Task<PaginatedList<OrderResponse>> GetAll(int pageIndex, int pageSize, int pharmacyId);
        Task<OrderResponse> UpdateOrder(int id,UpdateOrderRequest request);
        Task<bool> DeleteOrder(int id);
        Task<OrderDetailByIdResponse> getById(int id);
    }
}
