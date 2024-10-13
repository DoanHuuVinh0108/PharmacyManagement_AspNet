using Microsoft.EntityFrameworkCore;
using PharmacyManagermentSystem.DbContext;
using PharmacyManagermentSystem.Model;
using PharmacyManagermentSystem.Request;
using PharmacyManagermentSystem.Response;

namespace PharmacyManagermentSystem.Services.MiniServiceMedicine
{
    public class MedicineService : IMedicineService
    {
        private readonly MyDbContext _Dbcontext;
        public MedicineService(MyDbContext Dbcontext)
        {
            _Dbcontext = Dbcontext;
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
            _Dbcontext.Medicines.Add(medicine);
            await _Dbcontext.SaveChangesAsync();
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
            var medicine = await _Dbcontext.Medicines.FindAsync(request.Id,request.BatchNumber,request.CategoryId);
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
            await _Dbcontext.SaveChangesAsync();
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
        public async Task<List<Medicine>> GetAll() {             
            return await _Dbcontext.Medicines.ToListAsync();
        }
        public async Task<bool> DeleteMedicine(DeleteMedicineRequest request)
        {
            var medicine = await _Dbcontext.Medicines.FindAsync(request.Id,request.BatchNumber,request.CategoryId);
            if (medicine == null)
            {
                return false;
            }
            _Dbcontext.Medicines.Remove(medicine);
            await _Dbcontext.SaveChangesAsync();
            return true;
        }

    }
}
