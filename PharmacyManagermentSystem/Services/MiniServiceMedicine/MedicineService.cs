using Microsoft.EntityFrameworkCore;
using PharmacyManagermentSystem.DbContext;
using PharmacyManagermentSystem.Model;
using PharmacyManagermentSystem.Request;
using PharmacyManagermentSystem.Response;
using System.Drawing.Printing;

namespace PharmacyManagermentSystem.Services.MiniServiceMedicine
{
    public class MedicineService : IMedicineService
    {
        private readonly MyDbContext _dbContext;

        public MedicineService(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<MedicineResponse> CreateMedicine(CreateMedicineRequest request)
        {
            var medicine = new Medicine
            {
                Id = request.Id,
                BatchNumber = request.BatchNumber,
                ManufacturingDate = request.ManufacturingDate,
                ExpiryDate = request.ExpiryDate,
                Quantity = request.Quantity,
                Status = request.Status,
                CategoryId = request.CategoryId,
                PharmacyId = request.PharmacyId
            };

            _dbContext.Medicines.Add(medicine);
            await _dbContext.SaveChangesAsync();

            return new MedicineResponse
            {
                Id = medicine.Id,
                BatchNumber = medicine.BatchNumber,
                ManufacturingDate = medicine.ManufacturingDate,
                ExpiryDate = medicine.ExpiryDate,
                Quantity = medicine.Quantity,
                Status = medicine.Status,
                CategoryId = medicine.CategoryId,
                PharmacyId = medicine.PharmacyId
            };
        }

        public async Task<MedicineResponse> UpdateMedicine(UpdateMedicineRequest request)
        {
            var medicine = await _dbContext.Medicines
                .FindAsync(request.Id, request.BatchNumber, request.CategoryId);

            if (medicine == null)
            {
                throw new Exception("Medicine not found");
            }

            medicine.Id = request.Id;
            medicine.ManufacturingDate = request.ManufacturingDate;
            medicine.ExpiryDate = request.ExpiryDate;
            medicine.Quantity = request.Quantity;
            medicine.Status = request.Status;
            medicine.CategoryId = request.CategoryId;
            medicine.PharmacyId = request.PharmacyId;
            medicine.BatchNumber = request.BatchNumber;

            await _dbContext.SaveChangesAsync();

            return new MedicineResponse
            {
                Id = medicine.Id,
                BatchNumber = medicine.BatchNumber,
                ManufacturingDate = medicine.ManufacturingDate,
                ExpiryDate = medicine.ExpiryDate,
                Quantity = medicine.Quantity,
                Status = medicine.Status,
                CategoryId = medicine.CategoryId,
                PharmacyId = medicine.PharmacyId
            };
        }

        public async Task<PaginatedList<Medicine>> GetAll(int pageIndex, int pageSize, int pharmacyId)
        {
            if (pageIndex < 1) pageIndex = 1;
            if (pageSize < 1) pageSize = 10;

            List<Medicine> medicines;
            int totalItems;

            if (pharmacyId != 0)
            {
                medicines = await _dbContext.Medicines
                    .Where(m => m.PharmacyId == pharmacyId && m.Quantity>0)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
                totalItems = await _dbContext.Medicines.CountAsync(m => m.PharmacyId == pharmacyId && m.Quantity>0);
            }
            else
            {
                medicines = await _dbContext.Medicines
                    .Where(m => m.Quantity>0)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                totalItems = await _dbContext.Medicines.CountAsync(m=>m.Quantity>0);
            }

            return new PaginatedList<Medicine>
            {
                Items = medicines,
                TotalItems = totalItems,
                Page = pageIndex,
                PageSize = pageSize
            };
        }

        public async Task<bool> DeleteMedicine(DeleteMedicineRequest request)
        {
            var medicine = await _dbContext.Medicines
                .FindAsync(request.Id, request.BatchNumber, request.CategoryId);

            if (medicine == null)
            {
                return false;
            }

            _dbContext.Medicines.Remove(medicine);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<List<string>> GetByCategoryId(string categoryId, int pharmacyId)
        {
            var uniqueMedicineNames = await _dbContext.Medicines
                .Where(x => x.CategoryId == categoryId && x.PharmacyId==pharmacyId)
                .Select(x => x.BatchNumber)
                .Distinct()
                .ToListAsync();

            return uniqueMedicineNames;
        }

        public async Task<List<string>> GetByBatchNumber(string batchNumber, string categoryId)
        {
            var medicines = await _dbContext.Medicines
                .Where(x => x.BatchNumber == batchNumber && x.CategoryId == categoryId && x.Quantity>0)
                .Select(x => x.Id)
                .ToListAsync();

            return medicines;
        }

        public async Task<int> GetQuantity(string batchNumber, string categoryId, string medicineId)
        {
            var quantity = await _dbContext.Medicines
                .Where(x => x.BatchNumber == batchNumber && x.CategoryId == categoryId && x.Id == medicineId)
                .Select(x => x.Quantity)
                .FirstOrDefaultAsync();

            return quantity;
        }
    }
}
