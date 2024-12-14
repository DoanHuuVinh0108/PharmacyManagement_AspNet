using Microsoft.EntityFrameworkCore;
using PharmacyManagermentSystem.DbContext;
using PharmacyManagermentSystem.Model;
using PharmacyManagermentSystem.Request;
using PharmacyManagermentSystem.Response;

namespace PharmacyManagermentSystem.Services.MiniServiceReturnSupplier
{
    public class ReturnSupplierService : IReturnSupplierService
    {
        private readonly MyDbContext _Dbcontext;
        public ReturnSupplierService(MyDbContext Dbcontext)
        {
            _Dbcontext = Dbcontext;
        }
        public async Task<ReturnSupplierResponse> CreateReturnSupplier(CreateReturnSupplierRequest request)
        {
            using (var transaction = _Dbcontext.Database.BeginTransaction())
            {
                try
                {
                    var medicine = await _Dbcontext.Medicines.FirstOrDefaultAsync(x => x.Id == request.MedicineId && x.CategoryId == request.CategoryId && x.BatchNumber == request.BatchNumber);
                    if (medicine == null)
                    {
                        throw new Exception("Medicine not found");
                    }

                    var returnSupplier = new ReturnSupplier
                    {
                        Description = request.Description,
                        Quantity = request.Quantity,
                        Status = request.Status,
                        CategoryId = request.CategoryId,
                        MedicineId = request.MedicineId,
                        BatchNumber = request.BatchNumber,
                        SupplierId = request.SupplierId,
                        EmployeeId = request.EmployeeId,
                        Price = request.Price,
                        Date = DateOnly.FromDateTime(DateTime.Now)
                    };

                    medicine.Quantity -= request.Quantity;
                    _Dbcontext.ReturnSuppliers.Add(returnSupplier);


                    await _Dbcontext.SaveChangesAsync();
                    transaction.Commit();

                    return new ReturnSupplierResponse
                    {
                        Description = returnSupplier.Description,
                        Quantity = returnSupplier.Quantity,
                        Status = returnSupplier.Status,
                        CategoryId = returnSupplier.CategoryId,
                        MedicineId = returnSupplier.MedicineId,
                        BatchNumber = returnSupplier.BatchNumber,
                        SupplierId = returnSupplier.SupplierId,
                        EmployeeId = returnSupplier.EmployeeId,
                        Price = returnSupplier.Price
                    };
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }
        public async Task<ReturnSupplierResponse> UpdateReturnSupplier( UpdateReturnSupplierRequest request)
        {
            var result = await _Dbcontext.ReturnSuppliers.FirstOrDefaultAsync(x => x.CategoryId == request.CategoryId && x.MedicineId == request.MedicineId && x.BatchNumber == request.BatchNumber);
            if (result == null)
            {
                throw new Exception("ReturnSupplier not found");
            }
            result.Description = request.Description;
            result.Quantity = request.Quantity;
            result.Status = request.Status;
            result.SupplierId = request.SupplierId;
            result.EmployeeId = request.EmployeeId;
            await _Dbcontext.SaveChangesAsync();
            return new ReturnSupplierResponse {
                Description = result.Description,
                Quantity = result.Quantity,
                Status = result.Status,
                CategoryId = result.CategoryId,
                MedicineId = result.MedicineId,
                BatchNumber = result.BatchNumber,
                SupplierId = result.SupplierId,
                EmployeeId = result.EmployeeId
            };
        }
        public async Task<PaginatedList<ReturnSupplierResponse>> GetAll(int pageIndex, int pageSize, int pharmacyId)
        {
            if (pageIndex < 1) pageIndex = 1;
            if (pageSize < 1) pageSize = 10;
            var totalItems = await _Dbcontext.ReturnSuppliers.Where(x => x.Medicine.PharmacyId==pharmacyId).CountAsync();
            var returnSuppliers = await _Dbcontext.ReturnSuppliers
                                             .Skip((pageIndex - 1) * pageSize)
                                             .Take(pageSize)
                                             .Include(x => x.Medicine)
                                             .Include(x => x.Supplier)
                                             .Include(x => x.Employee)
                                             .Where(x => x.Medicine.PharmacyId == pharmacyId)
                                             .Select(x => new ReturnSupplierResponse
                                             {
                                                 Description = x.Description,
                                                 Quantity = x.Quantity,
                                                 Status = x.Status,
                                                 CategoryId = x.CategoryId,
                                                 MedicineId = x.MedicineId,
                                                 BatchNumber = x.BatchNumber,
                                                 SupplierId = x.SupplierId,
                                                 EmployeeId = x.EmployeeId,
                                                 CategoryName = x.Medicine.Category.MedicineName,
                                                 SupplierName = x.Supplier.Name,
                                                 EmployeeName = x.Employee.FullName,
                                                 Price = x.Price
                                             }).ToListAsync();
            return new PaginatedList<ReturnSupplierResponse>
            {
                Items = returnSuppliers,
                TotalItems = totalItems,
                Page = pageIndex,
                PageSize = pageSize
            };
           
        }
        public async Task<bool> Delete(DeleteReturnSupplierRequest request)
        {
            var result = await _Dbcontext.ReturnSuppliers.FirstOrDefaultAsync(x => x.CategoryId == request.CategoryId && x.MedicineId == request.MedicineId && x.BatchNumber == request.BatchNumber);
            if (result == null)
            {
                throw new Exception("ReturnSupplier not found");
            }
            var medicine = await _Dbcontext.Medicines.FirstOrDefaultAsync(x => x.Id == request.MedicineId && x.CategoryId == request.CategoryId && x.BatchNumber == request.BatchNumber);
            if (medicine == null)
            {
                throw new Exception("Medicine not found");
            }
            medicine.Quantity += result.Quantity;
            _Dbcontext.ReturnSuppliers.Remove(result);
            await _Dbcontext.SaveChangesAsync();
            return true;
        }

    }
}
