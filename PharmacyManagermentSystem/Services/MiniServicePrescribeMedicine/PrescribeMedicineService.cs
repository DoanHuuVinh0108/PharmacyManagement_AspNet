using CloudinaryDotNet.Core;
using Microsoft.EntityFrameworkCore;
using PharmacyManagermentSystem.DbContext;
using PharmacyManagermentSystem.Model;
using PharmacyManagermentSystem.Request;
using PharmacyManagermentSystem.Response;

namespace PharmacyManagermentSystem.Services.MiniServicePrescribeMedicine
{
    public class PrescribeMedicineService : IPrescribeMedicineService
    {
        private readonly MyDbContext _DbContext;
        public PrescribeMedicineService(MyDbContext dbContext)
        {
            _DbContext = dbContext;
        }
        public async Task<PrescribeMedicineResponse> CreatePrescribeMedicine (CreatePrescribeMedicineRequest request)
        {
            var prescribeMedicine = new PrescribeMedicine
            {
                MedicineId = request.MedicineId,
                MedicineName = request.MedicineName,
                Quantity = request.Quantity, 
                PrecsriptionId = request.PrecsriptionId
            };
            _DbContext.PrescribeMedicines.Add(prescribeMedicine);
            await _DbContext.SaveChangesAsync();
            return new PrescribeMedicineResponse
            {
                MedicineName = prescribeMedicine.MedicineName,
                MedicineId = prescribeMedicine.MedicineId,
                Quantity = prescribeMedicine.Quantity,
                PrecsriptionId = prescribeMedicine.PrecsriptionId
            };
        }
        public async Task<PrescribeMedicineResponse> UpdatePrescribeMedicine (UpdatePrescribeMedicineRequest request)
        {
            var prescribeMedicine = await _DbContext.PrescribeMedicines.FindAsync(request.MedicineId,request.PrecsriptionId);
            if (prescribeMedicine == null)
            {
                throw new Exception("PrescribeMedicine not found");
            }
            prescribeMedicine.Quantity = request.Quantity;
            await _DbContext.SaveChangesAsync();
            return new PrescribeMedicineResponse
            {
                MedicineName = prescribeMedicine.MedicineName,
                MedicineId = prescribeMedicine.MedicineId,
                Quantity = prescribeMedicine.Quantity,
                PrecsriptionId = prescribeMedicine.PrecsriptionId
            };
        }
        public async Task<List<PrescribeMedicine>> GetAll()
        {
            var prescribeMedicines = await _DbContext.PrescribeMedicines.ToListAsync();
            return prescribeMedicines;
        }
        public async Task<PrescribeMedicineResponse> DeletePrescribeMedicine(DeletePrescribeMedicineRequest request)
        {
            var prescribeMedicine = await _DbContext.PrescribeMedicines.FindAsync(request.MedicineId, request.PrecsriptionId);
            if (prescribeMedicine == null)
            {
                throw new Exception("PrescribeMedicine not found");
            }
            _DbContext.PrescribeMedicines.Remove(prescribeMedicine);
            await _DbContext.SaveChangesAsync();
            return new PrescribeMedicineResponse
            {
                MedicineName = prescribeMedicine.MedicineName,
                MedicineId = prescribeMedicine.MedicineId,
                Quantity = prescribeMedicine.Quantity,
                PrecsriptionId = prescribeMedicine.PrecsriptionId
            };
        }
    }
}
