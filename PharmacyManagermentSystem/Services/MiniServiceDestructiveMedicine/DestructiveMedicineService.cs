using Microsoft.EntityFrameworkCore;
using PharmacyManagermentSystem.DbContext;
using PharmacyManagermentSystem.Model;
using PharmacyManagermentSystem.Request;
using PharmacyManagermentSystem.Response;
using System.Linq;


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

            using (var transaction = _DbContext.Database.BeginTransaction())
            {
                try
                {
                    var medicine = await _DbContext.Medicines.Where(
                        x => x.Id == request.MedicineId &&
                        x.BatchNumber == request.BatchNumber &&
                        x.CategoryId == request.CategoryId
                    ).FirstOrDefaultAsync();
                    if (medicine == null)
                    {
                        throw new Exception("Medicine not found");
                    }
                    medicine.Quantity -= request.Quantity;
                    await _DbContext.SaveChangesAsync();
                    var DesMedicine = new DestructiveMedicine
                    {
                        Quantity = request.Quantity,
                        Date = DateOnly.FromDateTime(DateTime.Now),
                        Description = request.Description,
                        CategoryId = request.CategoryId,
                        MedicineId = request.MedicineId,
                        BatchNumber = request.BatchNumber,
                        EmployeeId = request.EmployeeId
                    };
                    await _DbContext.DestructiveMedicines.AddAsync(DesMedicine);
                    await _DbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return new DestructiveMedicineResponse
                    {
                        Quantity = DesMedicine.Quantity,
                        Date = DesMedicine.Date,
                        Description = DesMedicine.Description,
                        CategoryId = DesMedicine.CategoryId,
                        MedicineId = DesMedicine.MedicineId,
                        BatchNumber = DesMedicine.BatchNumber,
                        EmployeeId = DesMedicine.EmployeeId
                    };
                }
                catch (Exception e)
                {
                    await transaction.RollbackAsync();
                    throw new Exception("Cannot create DestructiveMedicine", e);
                }
            }
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
            request.Date = request.Date;
            result.Description = request.Description;
            result.EmployeeId = request.EmployeeId;
            await _DbContext.SaveChangesAsync();
            return new DestructiveMedicineResponse
            {
                Quantity = result.Quantity,
                Date = result.Date,
                Description = result.Description,
                CategoryId = result.CategoryId,
                MedicineId = result.MedicineId,
                BatchNumber = result.BatchNumber,
                EmployeeId = result.EmployeeId
            };
        }
        public async Task<PaginatedList<DestructiveMedicineResponse>> GetAll(int pageIndex, int pageSize, int pharmacyId)
        {
            if (pageIndex < 1) pageIndex = 1;
            if (pageSize < 1) pageSize = 10;

            // Get total count of items that match the filter
            int totalItems = await _DbContext.DestructiveMedicines
                .Where(dm => dm.Medicine.PharmacyId == pharmacyId) 
                .CountAsync();

            // Paginated result with filtering and inclusion
            var result = await _DbContext.DestructiveMedicines
                .Where(dm => dm.Medicine.PharmacyId == pharmacyId)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(dm => new DestructiveMedicineResponse
                {
                    Quantity = dm.Quantity,
                    Date = dm.Date,
                    Description = dm.Description,
                    CategoryId = dm.CategoryId,
                    MedicineId = dm.MedicineId,
                    BatchNumber = dm.BatchNumber,
                    EmployeeId = dm.EmployeeId,
                    EmployeeName = dm.Employee.FullName,
                    MedicineName = dm.Medicine.Category.MedicineName
                })
                .ToListAsync();
            
            // Map to your response type
            //var response = result.Select(dm => new DestructiveMedicineResponse
            //{
            //    Quantity = dm.Quantity,
            //    Date = dm.Date,
            //    Description = dm.Description,
            //    CategoryId = dm.CategoryId,
            //    MedicineId = dm.MedicineId,
            //    BatchNumber = dm.BatchNumber,
            //    EmployeeId = dm.EmployeeId,
            //}).ToList();

            return new PaginatedList<DestructiveMedicineResponse>
            {
                Items = result,
                Page = pageIndex,
                PageSize = pageSize,
                TotalItems = totalItems
            };
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
            var medicine = await _DbContext.Medicines.FirstOrDefaultAsync(x => x.Id == request.MedicineId && x.CategoryId == request.CategoryId && x.BatchNumber == request.BatchNumber);
            if (medicine == null)
            {
                throw new Exception("Medicine not found");
            }
            medicine.Quantity += result.Quantity;
            _DbContext.DestructiveMedicines.Remove(result);
            await _DbContext.SaveChangesAsync();
            return true;
        }
    }
}
