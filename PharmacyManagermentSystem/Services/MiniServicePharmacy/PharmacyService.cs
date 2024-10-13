using Microsoft.EntityFrameworkCore;
using PharmacyManagermentSystem.DbContext;
using PharmacyManagermentSystem.Model;
using PharmacyManagermentSystem.Request;
using PharmacyManagermentSystem.Response;
using System.ComponentModel;

namespace PharmacyManagermentSystem.Services.MiniServicePharmacy
{
    public class PharmacyService : IPharmacyService
    {
        private readonly MyDbContext _dbContext;
        public PharmacyService(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<PharmacyResponse> CreatePharmacy(CreatePharmacyRequest payload)
        {
            var result = await _dbContext.Pharmacies.AddAsync(new Pharmacy
            {
                Name = payload.Name,
                Address = payload.Address,
                PhoneNumber = payload.PhoneNumber,
                Email = payload.Email
            });
            await _dbContext.SaveChangesAsync();
            return new PharmacyResponse
            {
                Name = result.Entity.Name,
                Address = result.Entity.Address,
                PhoneNumber = result.Entity.PhoneNumber,
                Email = result.Entity.Email
            };
        }
        public async Task<PharmacyResponse> UpdatePharmacy(int id, UpdatePharmacyRequest payload)
        {
            var pharmacy = await _dbContext.Pharmacies.FindAsync(id);
            if (pharmacy == null)
            {
                throw new Exception("Pharmacy not found");
            }
            pharmacy.Name = payload.Name;
            pharmacy.Address = payload.Address;
            pharmacy.PhoneNumber = payload.PhoneNumber;
            pharmacy.Email = payload.Email;
            await _dbContext.SaveChangesAsync();
            return new PharmacyResponse
            {
                Name = pharmacy.Name,
                Address = pharmacy.Address,
                PhoneNumber = pharmacy.PhoneNumber,
                Email = pharmacy.Email
            };
        }
        public async Task<List<Pharmacy>> GetAllPharmacy()
        {
            var pharmacies = await _dbContext.Pharmacies.ToListAsync();
            return pharmacies;
        }
        public async Task<bool> DeletePharmacy(int id)
        {
            var pharmacy = await _dbContext.Pharmacies.FindAsync(id);
            if (pharmacy == null)
            {
                throw new Exception("Pharmacy not found");
            }
            _dbContext.Pharmacies.Remove(pharmacy);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
