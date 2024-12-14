using PharmacyManagermentSystem.Model;
using PharmacyManagermentSystem.Request;
using PharmacyManagermentSystem.Response;

namespace PharmacyManagermentSystem.Services.MiniServiceDoctor
{
    public interface IDoctorService
    {
        Task<DoctorResponse> CreateDoctor(CreateDoctorRequest payload);
        Task<DoctorResponse> UpdateDoctor(int id, UpdateDoctorRequest payload);
        Task<PaginatedList<Doctor>> GetAllDoctor(int pageIndex, int pageSize);
        Task<bool> DeleteDoctor(int id);
    }
}
