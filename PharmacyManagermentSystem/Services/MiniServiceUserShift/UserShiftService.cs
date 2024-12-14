using Microsoft.EntityFrameworkCore;
using PharmacyManagermentSystem.DbContext;
using PharmacyManagermentSystem.Model;
using PharmacyManagermentSystem.Request;
using PharmacyManagermentSystem.Response;

namespace PharmacyManagermentSystem.Services.MiniServiceUserShift
{
    public class UserShiftService : IUserShiftService
    {
        private readonly MyDbContext _Dbcontext;
        public UserShiftService(MyDbContext Dbcontext)
        {
            _Dbcontext = Dbcontext;
        }
        public async Task<UserShiftResponse> CreateUserShift(CreateUserShiftRequest request)
        {
            using (var transaction = _Dbcontext.Database.BeginTransaction())
            {
                try
                {
                    var userShift = new UserShift
                    {
                        EmployeeId = request.EmployeeId,
                        Date = request.Date,
                        PharmacyId = request.PharmacyId
                    };
                    await _Dbcontext.UserShifts.AddAsync(userShift);
                    var shift = await _Dbcontext.Shifts.FindAsync(request.Date, request.PharmacyId);
                    if (shift != null)
                    {
                        shift.Count++;
                    }

                    await _Dbcontext.SaveChangesAsync();
                    transaction.Commit();
                    return new UserShiftResponse
                    {
                        EmployeeId = userShift.EmployeeId,
                        Date = userShift.Date,
                        PharmacyId = userShift.PharmacyId
                    };
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw e;
                }
            }

        }
        public async Task<UserShiftResponse> UpdateUserShift(UpdateUserShiftRequest request)
        {
            var userShift = await _Dbcontext.UserShifts.FindAsync(request.EmployeeId, request.Date, request.PharmacyId);
            if (userShift == null)
            {
                throw new Exception("UserShift not found");
            }
            userShift.EmployeeId = request.NewEmployeeId;
            userShift.Date = request.NewDate;
            userShift.PharmacyId = request.NewPharmacyId;
            await _Dbcontext.SaveChangesAsync();
            return new UserShiftResponse
            {
                EmployeeId = userShift.EmployeeId,
                Date = userShift.Date,
                PharmacyId = userShift.PharmacyId
            };
        }
        public async Task<List<UserShift>> GetAll(DateOnly from, DateOnly to, int pharmacyId)
        {
            return await _Dbcontext.UserShifts.Where(x => x.Date >= from && x.Date <= to && x.PharmacyId == pharmacyId).ToListAsync();
        }
        public async Task<bool> DeleteUserShift(DeleteUserShiftRequest request)
        {
            var userShift = await _Dbcontext.UserShifts.FindAsync(request.EmployeeId, request.Date, request.PharmacyId);
            if (userShift == null)
            {
                return false;
            }
            _Dbcontext.UserShifts.Remove(userShift);
            await _Dbcontext.SaveChangesAsync();
            return true;

        }
        public async Task<List<UserShift>> GetById(string id)
        {
            return await _Dbcontext.UserShifts.Where(x => x.EmployeeId == id).ToListAsync();
        }
        public async Task<List<UserShiftByDateResponse>> GetByDate(DateOnly date,int pharmacyId)
        {
            return await _Dbcontext.UserShifts
                .Include(x => x.Employee)
                .Where(x => x.Date == date && x.PharmacyId==pharmacyId)
                .Select(x => new UserShiftByDateResponse
                {
                    EmployeeId = x.EmployeeId,
                    Date = x.Date,
                    PharmacyId = x.PharmacyId,
                    FullName = x.Employee.FullName,
                    Email = x.Employee.Email,
                    PhoneNumber = x.Employee.PhoneNumber
                })
                .ToListAsync();
        }

    }
}
