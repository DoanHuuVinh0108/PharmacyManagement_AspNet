using Microsoft.AspNetCore.Razor.Language.Intermediate;
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
        {
            using (var transaction = _DbContext.Database.BeginTransaction())
            {
                if (request.Status == null)
                {
                    request.Status = "Pending";
                }
                //if (request.Status != "Pending" && request.Status != "Processing" && request.Status != "Completed" && request.Status != "Cancelled")
                //{
                //    throw new Exception("Invalid Status");
                //}
                try
                {
                    var order = new Order
                    {
                        Status = request.Status,
                        PharmacyId = request.PharmacyId,
                        CustomerId = request.CustomerId,
                        EmployeeId = request.EmployeeId,
                        PrescriptionId = request.PrescriptionId,
                        TotalPrice = request.TotalPrice,
                        Date = DateOnly.FromDateTime(DateTime.Now)
                    };
                    var Employee = await _DbContext.Users.FindAsync(request.EmployeeId);
                    if(Employee.PharmacyId != request.PharmacyId)
                    {
                        throw new Exception("Nhân viên này không thuộc nhà thuốc");
                    }
                    _DbContext.Orders.Add(order);
                    await _DbContext.SaveChangesAsync();
                    foreach (var item in request.createOrderDetailRequests)
                    {
                        var orderDetail = new OrderDetail
                        {
                            OrderId = order.Id,
                            MedicineId = item.MedicineId,
                            Quantity = item.Quantity,
                            Price = item.Price,
                            CategoryId = item.CategoryId,
                            BatchNumber = item.BatchNumber
                        };
                        _DbContext.OrderDetails.Add(orderDetail);
                        var medicine = await _DbContext.Medicines.Where(
                            x => x.Id == item.MedicineId &&
                            x.BatchNumber == item.BatchNumber &&
                            x.CategoryId == item.CategoryId
                        ).FirstOrDefaultAsync();
                        if (medicine == null)
                        {
                            throw new Exception("Medicine not found");
                        }
                        medicine.Quantity -= item.Quantity;

                    }
                    await _DbContext.SaveChangesAsync();
                    transaction.Commit();
                    return new OrderResponse
                    {
                        Id = order.Id,
                        Status = order.Status,
                        PharmacyId = order.PharmacyId,
                        CustomerId = order.CustomerId,
                        EmployeeId = order.EmployeeId,
                        PrescriptionId = order.PrescriptionId,
                        TotalPrice = order.TotalPrice,

                    };
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    return null;
                }
            }

        }
        public async Task<PaginatedList<OrderResponse>> GetAll(int pageIndex, int pageSize, int pharmacyId)
        {
            if (pageIndex < 1) pageIndex = 1;
            if (pageSize < 1) pageSize = 10;
            var totalItems = await _DbContext.Orders.Where(x => x.PharmacyId == pharmacyId).CountAsync();
            var orders = await _DbContext.Orders
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Where(x => x.PharmacyId == pharmacyId)
                .Include(x => x.Customer)
                .Include(x => x.Employee)
                .Select(x => new OrderResponse
                {
                    Id = x.Id,
                    Status = x.Status,
                    PharmacyId = x.PharmacyId,
                    CustomerId = x.CustomerId,
                    CustomerName = x.Customer.FullName,
                    EmployeeId = x.EmployeeId,
                    EmployeeName = x.Employee.FullName,
                    PrescriptionId = x.PrescriptionId,
                    TotalPrice = x.TotalPrice,
                    Date = x.Date
                })
                .ToListAsync();
            return new PaginatedList<OrderResponse>
            {
                Items = orders,
                TotalItems = totalItems,
                Page = pageIndex,
                PageSize = pageSize
            };
        }
        public async Task<OrderResponse> UpdateOrder(int id, UpdateOrderRequest request)
        {
            var order = await _DbContext.Orders.FindAsync(id);
            if (order == null)
            {
                throw new Exception("Order not found");
            }
            if (request.Status != "Pending" && request.Status != "Processing" && request.Status != "Completed" && request.Status != "Cancelled")
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
            if (order == null)
            {
                throw new Exception("Order not found");
            }
            _DbContext.Orders.Remove(order);
            await _DbContext.SaveChangesAsync();
            return true;
        }
        public async Task<OrderDetailByIdResponse> getById(int id)
        {
            var orderResult = await _DbContext.Orders
           .Where(x => x.Id == id)
           .Include(x => x.Customer)
           .Include(x => x.Employee)
           .Include(x => x.Pharmacy)
           .Select(x => new OrderResponse
           {
               Id = x.Id,
               Status = x.Status,
               PharmacyId = x.PharmacyId,
               PharmacyName = x.Pharmacy.Name,
               CustomerId = x.CustomerId,
               CustomerName = x.Customer.FullName,
               EmployeeId = x.EmployeeId,
               EmployeeName = x.Employee.FullName,
               PrescriptionId = x.PrescriptionId,
               TotalPrice = x.TotalPrice,
           })
           .FirstOrDefaultAsync();



            if (orderResult == null)
            {
                throw new Exception("Order not found");
            }
            var orderDetails = await _DbContext.OrderDetails
                .Where(x => x.OrderId == id)
                .Include(x => x.Medicine)
                .Select(x => new OrderDetailResponse
                {
                    Quantity = x.Quantity,
                    Price = x.Price,
                    OrderId = x.OrderId,
                    CategoryId = x.Medicine.CategoryId,
                    CategoryName = x.Medicine.Category.MedicineName,
                    MedicineId = x.MedicineId,
                    BatchNumber = x.BatchNumber
                })
                .ToListAsync();
            return new OrderDetailByIdResponse
            {
                order = orderResult,
                orderDetails = orderDetails
            };
        }
    }
}
