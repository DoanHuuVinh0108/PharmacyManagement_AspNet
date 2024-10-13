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
        public async Task<ReturnSupplierResponse> CreateReturnSupplier (CreateReturnSupplierRequest request)
        {
            var returnSupplier = new ReturnSupplier
            {
                Description = request.Description,
                Quantity = request.Quantity,
                Status = request.Status,
                CategoryId = request.CategoryId,
                MedicineId = request.MedicineId,
                BatchNumber = request.BatchNumber,
                SupplierId = request.SupplierId,
                EmployeeId = request.EmployeeId
            };
            _Dbcontext.ReturnSuppliers.Add(returnSupplier);
            await _Dbcontext.SaveChangesAsync();
            return new ReturnSupplierResponse
            {
                Description = returnSupplier.Description,
                Quantity = returnSupplier.Quantity,
                Status = returnSupplier.Status,
                CategoryId = returnSupplier.CategoryId,
                MedicineId = returnSupplier.MedicineId,
                BatchNumber = returnSupplier.BatchNumber,
                SupplierId = returnSupplier.SupplierId,
                EmployeeId = returnSupplier.EmployeeId
            };

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
        public async Task<List<ReturnSupplier>> GetAll()
        {
           var returnSuppliers = await _Dbcontext.ReturnSuppliers.ToListAsync();
           return returnSuppliers;
        }
        public async Task<bool> Delete(DeleteReturnSupplierRequest request)
        {
            var result = await _Dbcontext.ReturnSuppliers.FirstOrDefaultAsync(x => x.CategoryId == request.CategoryId && x.MedicineId == request.MedicineId && x.BatchNumber == request.BatchNumber);
            if (result == null)
            {
                throw new Exception("ReturnSupplier not found");
            }
            _Dbcontext.ReturnSuppliers.Remove(result);
            await _Dbcontext.SaveChangesAsync();
            return true;
        }

    }
}
