using Microsoft.EntityFrameworkCore;
using PharmacyManagermentSystem.DbContext;
using PharmacyManagermentSystem.Model;
using PharmacyManagermentSystem.Request;
using PharmacyManagermentSystem.Response;

namespace PharmacyManagermentSystem.Services.MiniServiceOrder
{
    public class OrderService : IOrderService
    {
        private readonly MyDbContext _DbContext;
        public OrderService(MyDbContext dbContext)
        {
            _DbContext = dbContext;
        }
        public async Task<OrderResponse> CreateOrder(CreateOrderRequest request)
        {   if(request.Status == null)
            {
                request.Status = "Pending";
            }
            if(request.Status != "Pending" && request.Status != "Processing" && request.Status != "Completed" && request.Status != "Cancelled")
            {
                throw new Exception("Invalid Status");
            }
            
            var order = new Order
            {
                Status = request.Status,
                PharmacyId = request.PharmacyId,
                CustomerId = request.CustomerId,
                EmployeeId = request.EmployeeId,
                PrescriptionId = request.PrescriptionId
            };
            _DbContext.Orders.Add(order);
            await _DbContext.SaveChangesAsync();
            return new OrderResponse
            {
                Id = order.Id,
                Status = order.Status,
                PharmacyId = order.PharmacyId,
                CustomerId = order.CustomerId,
                EmployeeId = order.EmployeeId,
                PrescriptionId = order.PrescriptionId
            };
        }
        public async Task<List<Order>> GetAll()
        {
            return await _DbContext.Orders.ToListAsync();
        }
        public async Task<OrderResponse> UpdateOrder(int id, UpdateOrderRequest request)
        {
            var order = await _DbContext.Orders.FindAsync(id);
            if(order == null)
            {
                throw new Exception("Order not found");
            }
            if(request.Status != "Pending" && request.Status != "Processing" && request.Status != "Completed" && request.Status != "Cancelled")
            {
                throw new Exception("Invalid Status");
            }
            order.Status = request.Status;
            order.CustomerId = request.CustomerId;
            order.EmployeeId = request.EmployeeId;
            order.PharmacyId = request.PharmacyId;
            order.PrescriptionId = request.PrescriptionId;
            await _DbContext.SaveChangesAsync();
            return new OrderResponse
            {
                Id = order.Id,
                Status = order.Status,
                PharmacyId = order.PharmacyId,
                CustomerId = order.CustomerId,
                EmployeeId = order.EmployeeId,
                PrescriptionId = order.PrescriptionId
            };
        }
        public async Task<bool> DeleteOrder(int id)
        {
            var order = await _DbContext.Orders.FirstOrDefaultAsync(x => x.Id == id);
            if(order == null)
            {
                throw new Exception("Order not found");
            }
            _DbContext.Orders.Remove(order);
            await _DbContext.SaveChangesAsync();
            return true;
        }
    }
}
