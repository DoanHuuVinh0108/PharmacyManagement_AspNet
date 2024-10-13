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
            var userShift = new UserShift
            {
                EmployeeId = request.EmployeeId,
                NameShift = request.NameShift,
                Date = request.Date,
                PharmacyId = request.PharmacyId
            };
            await _Dbcontext.UserShifts.AddAsync(userShift);
            await _Dbcontext.SaveChangesAsync();
            return new UserShiftResponse
            {
                EmployeeId = userShift.EmployeeId,
                NameShift = userShift.NameShift,
                Date = userShift.Date,
                PharmacyId = userShift.PharmacyId
            };
        }
        public async Task<UserShiftResponse> UpdateUserShift(UpdateUserShiftRequest request)
        {
            var userShift = await _Dbcontext.UserShifts.FindAsync(request.EmployeeId, request.Date, request.NameShift, request.PharmacyId);
            if (userShift == null)
            {
                throw new Exception("UserShift not found");
            }
            userShift.EmployeeId = request.NewEmployeeId;
            userShift.NameShift = request.NewNameShift;
            userShift.Date = request.NewDate;
            userShift.PharmacyId = request.NewPharmacyId;
            await _Dbcontext.SaveChangesAsync();
            return new UserShiftResponse
            {
                EmployeeId = userShift.EmployeeId,
                NameShift = userShift.NameShift,
                Date = userShift.Date,
                PharmacyId = userShift.PharmacyId
            };
        }
        public async Task<List<UserShift>> GetAll()
        {
            return await _Dbcontext.UserShifts.ToListAsync();
        }
        public async Task<bool> DeleteUserShift(DeleteUserShiftRequest request)
        {
            var userShift = await _Dbcontext.UserShifts.FindAsync(request.EmployeeId, request.Date, request.NameShift, request.PharmacyId);
            if (userShift == null)
            {
                return false;
            }
            _Dbcontext.UserShifts.Remove(userShift);
            await _Dbcontext.SaveChangesAsync();
            return true;
          
        }
    }
}
