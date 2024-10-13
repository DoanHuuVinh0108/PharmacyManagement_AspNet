using PharmacyManagermentSystem.Model;
using PharmacyManagermentSystem.Request;
using PharmacyManagermentSystem.Response;

namespace PharmacyManagermentSystem.Services.MiniServiceSalary
{
    public interface ISalaryService
    {
        Task<SalaryResponse> CreateSalary(CreateSalaryRequest payload);
        Task<SalaryResponse> UpdateSalary(UpdateSalaryRequest payload);
        Task<List<Salary>> GetAllSalary();
        Task<bool> DeleteSalary(DeleteSalaryRequest payload);
    }
}
