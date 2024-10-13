using Microsoft.EntityFrameworkCore;
using PharmacyManagermentSystem.DbContext;
using PharmacyManagermentSystem.Model;
using PharmacyManagermentSystem.Request;
using PharmacyManagermentSystem.Response;

namespace PharmacyManagermentSystem.Services.MiniServiceOrderDetail
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly MyDbContext _DbContext;
        public OrderDetailService(MyDbContext dbContext)
        {
            _DbContext = dbContext;
        }
        public async Task<OrderDetailResponse> CreateOrderDetail(CreateOrderDetailRequest request)
        {
            var orderDetail = new OrderDetail
            {
                Quantity = request.Quantity,
                Price = request.Price,
                OrderId = request.OrderId,
                CategoryId = request.CategoryId,
                MedicineId = request.MedicineId,
                BatchNumber = request.BatchNumber
            };
            var test1 = await _DbContext.OrderDetails.AddAsync(orderDetail);
            Console.WriteLine(test1);
            var test2 = await _DbContext.SaveChangesAsync();
            Console.WriteLine(test2);
            return new OrderDetailResponse
            {
                Quantity = orderDetail.Quantity,
                Price = orderDetail.Price,
                OrderId = orderDetail.OrderId,
                CategoryId = orderDetail.CategoryId,
                MedicineId = orderDetail.MedicineId,
                BatchNumber = orderDetail.BatchNumber
            };
        }
        public async Task<OrderDetailResponse> UpdateOrderDetail (UpdateOrderDetailRequest request)
        {
            var OrderDetail = await _DbContext.OrderDetails.FirstOrDefaultAsync(od=> 
            od.OrderId==request.OrderId && 
            od.MedicineId==request.MedicineId &&
            od.CategoryId==request.CategoryId &&
            od.BatchNumber==request.BatchNumber
            );
            if (OrderDetail == null)
            {
                throw new Exception("OrderDetail not found");
            }
            OrderDetail.Quantity = request.Quantity;
            OrderDetail.Price = request.Price;
            OrderDetail.OrderId = request.OrderId;
            OrderDetail.CategoryId = request.NewCategoryId ?? request.CategoryId;
            OrderDetail.MedicineId = request.NewMedicineId ?? request.MedicineId;
            OrderDetail.BatchNumber = request.NewBatchNumber ?? request.BatchNumber;
            await _DbContext.SaveChangesAsync();
            return new OrderDetailResponse
            {
                Quantity = OrderDetail.Quantity,
                Price = OrderDetail.Price,
                OrderId = OrderDetail.OrderId,
                CategoryId = OrderDetail.CategoryId,
                MedicineId = OrderDetail.MedicineId,
                BatchNumber = OrderDetail.BatchNumber
            };
        }
        public async Task<List<OrderDetail>> GetAll()
        {
            return await _DbContext.OrderDetails.ToListAsync();
        }
        public async Task<bool> DeleteOrderDetail(DeleteOrderDetailRequest request)
        {
            var OrderDetail = await _DbContext.OrderDetails.FirstOrDefaultAsync(od =>
               od.OrderId == request.OrderId &&
               od.MedicineId == request.MedicineId &&
               od.CategoryId == request.CategoryId &&
               od.BatchNumber == request.BatchNumber
               );
            if (OrderDetail == null)
            {
                throw new Exception("OrderDetail not found");
            }
            _DbContext.OrderDetails.Remove(OrderDetail);
            await _DbContext.SaveChangesAsync();
            return true;
        }
    }
}
