using PharmacyManagermentSystem.Model;
using PharmacyManagermentSystem.Request;
using PharmacyManagermentSystem.Response;

namespace PharmacyManagermentSystem.Services.MiniServiceOrder
{
    public interface IOrderService
    {
        Task<OrderResponse> CreateOrder(CreateOrderRequest request);
        Task<List<Order>> GetAll();
        Task<OrderResponse> UpdateOrder(int id,UpdateOrderRequest request);
        Task<bool> DeleteOrder(int id);
    }
}
