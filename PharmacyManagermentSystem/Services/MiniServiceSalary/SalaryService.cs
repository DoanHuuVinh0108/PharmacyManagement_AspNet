using PharmacyManagermentSystem.DbContext;
using PharmacyManagermentSystem.Request;
using PharmacyManagermentSystem.Response;
using PharmacyManagermentSystem.Model;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;

namespace PharmacyManagermentSystem.Services.MiniServiceSalary
{
    public class SalaryService :ISalaryService
    {
        private readonly MyDbContext _Dbcontext;
        public SalaryService(MyDbContext Dbcontext)
        {
            _Dbcontext = Dbcontext;
        }
        public async Task<SalaryResponse> CreateSalary(CreateSalaryRequest payload)
        {
            var Salary = await _Dbcontext.Salarys.AddAsync(new Salary
            {
                Month = payload.Month,
                Year = payload.Year,
                BasicSalary = payload.BasicSalary,
                Bonus = payload.Bonus,
                DayWorked = payload.DayWorked,
                DayOff = payload.DayOff,
                EmployeeId = payload.EmployeeId
            });
            await _Dbcontext.SaveChangesAsync();
            return new SalaryResponse
            {
                Month = Salary.Entity.Month,
                Year = Salary.Entity.Year,
                BasicSalary = Salary.Entity.BasicSalary,
                Bonus = Salary.Entity.Bonus,
                DayWorked = Salary.Entity.DayWorked,
                DayOff = Salary.Entity.DayOff,
                EmployeeId = Salary.Entity.EmployeeId
            };
        }
        public async Task<SalaryResponse> UpdateSalary( UpdateSalaryRequest payload)
        {
            var Salary = await _Dbcontext.Salarys.FindAsync(payload.Month,payload.Year,payload.EmployeeId);
            if (Salary == null)
            {
                throw new Exception("Salary not found");
            }
            Salary.Month = payload.Month;
            Salary.Year = payload.Year;
            Salary.BasicSalary = payload.BasicSalary;
            Salary.Bonus = payload.Bonus;
            Salary.DayWorked = payload.DayWorked;
            Salary.DayOff = payload.DayOff;
            Salary.EmployeeId = payload.EmployeeId;
            await _Dbcontext.SaveChangesAsync();
            return new SalaryResponse
            {
                Month = Salary.Month,
                Year = Salary.Year,
                BasicSalary = Salary.BasicSalary,
                Bonus = Salary.Bonus,
                DayWorked = Salary.DayWorked,
                DayOff = Salary.DayOff,
                EmployeeId = Salary.EmployeeId
            };
        }

        public async Task<PaginatedList<SalaryResponse>> GetAllSalary(int pageIndex, int pageSize,int pharmacyId)
        {
            if (pageIndex < 1) pageIndex = 1;
            if (pageSize < 1) pageSize = 10;

            var totalItems = await _Dbcontext.Salarys.Where(x => x.Employee.PharmacyId == pharmacyId).CountAsync();
            var Salarys = await _Dbcontext.Salarys.Include(x => x.Employee).Where(x => x.Employee.PharmacyId == pharmacyId).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedList<SalaryResponse>
            {
                Items = Salarys.Select(x => new SalaryResponse
                {
                    Month = x.Month,
                    Year = x.Year,
                    BasicSalary = x.BasicSalary,
                    Bonus = x.Bonus,
                    DayWorked = x.DayWorked,
                    DayOff = x.DayOff,
                    EmployeeId = x.EmployeeId,
                    EmployeeName = x.Employee.FullName
                }).ToList(),
                TotalItems = totalItems,
                Page = pageIndex,
                PageSize = pageSize
            };
            
        }
        public async Task<bool> DeleteSalary(int Month, int Year, string EmployeeId)
        {
            var Salary = await _Dbcontext.Salarys.FindAsync(Month,Year,EmployeeId);
            if (Salary == null)
            {
                throw new Exception("Salary not found");
            }
            _Dbcontext.Salarys.Remove(Salary);
            await _Dbcontext.SaveChangesAsync();
            return true;
        }
    } 
}
