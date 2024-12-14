using PharmacyManagermentSystem.Model;
using PharmacyManagermentSystem.Request;
using PharmacyManagermentSystem.Response;

namespace PharmacyManagermentSystem.Services.MiniServiceSalary
{
    public interface ISalaryService
    {
        Task<SalaryResponse> CreateSalary(CreateSalaryRequest payload);
        Task<SalaryResponse> UpdateSalary(UpdateSalaryRequest payload);
        Task<PaginatedList<SalaryResponse>> GetAllSalary(int pageIndex, int pageSize, int pharmacyId);
        Task<bool> DeleteSalary(int Month, int Year, string EmployeeId);
    }
}
