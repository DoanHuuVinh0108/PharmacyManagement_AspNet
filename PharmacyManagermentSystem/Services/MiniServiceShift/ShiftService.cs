using Microsoft.EntityFrameworkCore;
using PharmacyManagermentSystem.DbContext;
using PharmacyManagermentSystem.Model;
using PharmacyManagermentSystem.Request;
using PharmacyManagermentSystem.Response;

namespace PharmacyManagermentSystem.Services.MiniServiceShift
{
    public class ShiftService : IShiftService
    {
        private readonly MyDbContext _Dbcontext;
        public ShiftService(MyDbContext context)
        {
            _Dbcontext = context;
        }
        public async Task<ShiftResponse> CreateShift(CreateShiftRequest request)
        {
            var shift = new Shift
            {
                Date = request.Date,
                NameShift = request.NameShift,
                Count = request.Count,
                Limit = request.Limit,
                PharmacyId = request.PharmacyId
            };
            await _Dbcontext.Shifts.AddAsync(shift);
            await _Dbcontext.SaveChangesAsync();
            return new ShiftResponse
            {
                Date = shift.Date,
                NameShift = shift.NameShift,
                Count = shift.Count,
                Limit = shift.Limit,
                PharmacyId = shift.PharmacyId
            };
        }
        public async Task<ShiftResponse> UpdateShift(UpdateShiftRequest request)
        {
            var shift = await _Dbcontext.Shifts.FindAsync(request.Date, request.NameShift, request.PharmacyId);
            if (shift == null)
            {
                return null;
            }
            shift.Count = request.Count;
            shift.Limit = request.Limit;
            await _Dbcontext.SaveChangesAsync();
            return new ShiftResponse
            {
                Date = shift.Date,
                NameShift = shift.NameShift,
                Count = shift.Count,
                Limit = shift.Limit,
                PharmacyId = shift.PharmacyId
            };
        }
        public async Task<List<Shift>> GetAll()
        {
           return await _Dbcontext.Shifts.ToListAsync();
        }
        public async Task<bool> DeleteShift(DeleteShiftRequest request)
        {
            var shift = await _Dbcontext.Shifts.FindAsync(request.Date, request.NameShift, request.PharmacyId);
            if (shift == null)
            {
                return false;
            }
            _Dbcontext.Shifts.Remove(shift);
            await _Dbcontext.SaveChangesAsync();
            return true;
        }

    }
}
