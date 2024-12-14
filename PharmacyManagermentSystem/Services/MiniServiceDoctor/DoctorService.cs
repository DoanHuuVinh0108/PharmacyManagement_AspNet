using Microsoft.EntityFrameworkCore;
using PharmacyManagermentSystem.DbContext;
using PharmacyManagermentSystem.Model;
using PharmacyManagermentSystem.Request;
using PharmacyManagermentSystem.Response;

namespace PharmacyManagermentSystem.Services.MiniServiceDoctor
{
    public class DoctorService : IDoctorService
    {
        private readonly MyDbContext _dbContext;
        public DoctorService(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<DoctorResponse> CreateDoctor(CreateDoctorRequest payload)
        {
            var result = await _dbContext.Doctors.AddAsync(new Doctor
            {
                Name = payload.Name,
                Address = payload.Address,
                PhoneNumber = payload.PhoneNumber,
                Email = payload.Email
            });
            await _dbContext.SaveChangesAsync();
            return new DoctorResponse
            {
                Name = result.Entity.Name,
                Address = result.Entity.Address,
                PhoneNumber = result.Entity.PhoneNumber,
                Email = result.Entity.Email
            };
        }
        public async Task<DoctorResponse> UpdateDoctor(int id, UpdateDoctorRequest payload)
        {
            var doctor = await _dbContext.Doctors.FindAsync(id);
            if (doctor == null)
            {
                throw new Exception("Doctor not found");
            }
            doctor.Name = payload.Name;
            doctor.Address = payload.Address;
            doctor.PhoneNumber = payload.PhoneNumber;
            doctor.Email = payload.Email;
            await _dbContext.SaveChangesAsync();
            return new DoctorResponse
            {
                Name = doctor.Name,
                Address = doctor.Address,
                PhoneNumber = doctor.PhoneNumber,
                Email = doctor.Email
            };
        }
        public async Task<PaginatedList<Doctor>> GetAllDoctor(int pageIndex, int pageSize)
        {
            if (pageIndex < 1) pageIndex = 1;
            if (pageSize < 1) pageSize = 10;
            var totalItems = await _dbContext.Doctors.CountAsync();
            var doctors = await _dbContext.Doctors.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedList<Doctor>{
                Items = doctors,
                TotalItems = totalItems,
                Page = pageIndex,
                PageSize = pageSize
            };
           
        }
        public async Task<bool> DeleteDoctor(int id)
        {
            var doctor = await _dbContext.Doctors.FindAsync(id);
            if (doctor == null)
            {
                throw new Exception("Doctor not found");
            }
            _dbContext.Doctors.Remove(doctor);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
