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
        public async Task<List<Salary>> GetAllSalary()
        {
            var Salarys = await _Dbcontext.Salarys.ToListAsync();
            return Salarys;
        }
        public async Task<bool> DeleteSalary(DeleteSalaryRequest payload)
        {
            var Salary = await _Dbcontext.Salarys.FindAsync(payload.Month,payload.Year,payload.EmployeeId);
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
