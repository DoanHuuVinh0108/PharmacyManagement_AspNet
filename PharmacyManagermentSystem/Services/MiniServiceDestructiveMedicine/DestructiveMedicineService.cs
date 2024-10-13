using Microsoft.EntityFrameworkCore;
using PharmacyManagermentSystem.DbContext;
using PharmacyManagermentSystem.Model;
using PharmacyManagermentSystem.Request;
using PharmacyManagermentSystem.Response;

namespace PharmacyManagermentSystem.Services.MiniServiceDestructiveMedicine
{
    public class DestructiveMedicineService : IDestructiveMedicineService
    {
        private readonly MyDbContext _DbContext;
        public DestructiveMedicineService(MyDbContext dbContext)
        {
            _DbContext = dbContext;
        }
        public async Task<DestructiveMedicineResponse> CreateDestructiveMedicine(CreateDestructiveMedicineRequest request)
        {
            var DesMedicine = new DestructiveMedicine
            {
                Quantity = request.Quantity,
                Status = request.Status,
                Date = request.Date,
                Description = request.Description,
                CategoryId = request.CategoryId,
                MedicineId = request.MedicineId,
                BatchNumber = request.BatchNumber,
                EmployeeId = request.EmployeeId
            };
            await _DbContext.DestructiveMedicines.AddAsync(DesMedicine);
            await _DbContext.SaveChangesAsync();
            return new DestructiveMedicineResponse
            {
                Quantity = DesMedicine.Quantity,
                Status = DesMedicine.Status,
                Date = DesMedicine.Date,
                Description = DesMedicine.Description,
                CategoryId = DesMedicine.CategoryId,
                MedicineId = DesMedicine.MedicineId,
                BatchNumber = DesMedicine.BatchNumber,
                EmployeeId = DesMedicine.EmployeeId
            };
        }
        public async Task<DestructiveMedicineResponse> UpdateDestructiveMedicine(UpdateDestructiveMedicineRequest request)
        {
            var result = await _DbContext.DestructiveMedicines.FirstOrDefaultAsync(
                dm =>
                dm.MedicineId == request.MedicineId &&
                dm.BatchNumber == request.BatchNumber &&
                dm.CategoryId == request.CategoryId
            );
            if (result == null)
            {
                throw new Exception("DestructiveMedicine not found");
            }
            result.Quantity = request.Quantity;
            result.Status = request.Status;
            request.Date = request.Date;
            result.Description = request.Description;
            result.EmployeeId = request.EmployeeId;
            await _DbContext.SaveChangesAsync();
            return new DestructiveMedicineResponse
            {
                Quantity = result.Quantity,
                Status = result.Status,
                Date = result.Date,
                Description = result.Description,
                CategoryId = result.CategoryId,
                MedicineId = result.MedicineId,
                BatchNumber = result.BatchNumber,
                EmployeeId = result.EmployeeId
            };
        }
        public async Task<List<DestructiveMedicine>> GetAll()
        {

           return await _DbContext.DestructiveMedicines.ToListAsync();
        }
        public async Task<bool> DeleteDestructiveMedicine(DeleteDestructiveMedicineRequest request)
        {
            var result = await _DbContext.DestructiveMedicines.FirstOrDefaultAsync(
                dm =>
                dm.MedicineId == request.MedicineId &&
                dm.BatchNumber == request.BatchNumber &&
                dm.CategoryId == request.CategoryId
                );
            if (result == null)
            {
                throw new Exception("DestructiveMedicine not found");
            }
            _DbContext.DestructiveMedicines.Remove(result);
            await _DbContext.SaveChangesAsync();
            return true;
        }
    }
}
