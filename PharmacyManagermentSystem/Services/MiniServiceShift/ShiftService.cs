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
        public async Task<bool> CreateShift(CreateShiftRequest request)
        {
            foreach (var item in request.Dates)
            {
                var shift = new Shift
                {
                    Date = item,
                    Count = request.Count,
                    Limit = request.Limit,
                    PharmacyId = request.PharmacyId
                };
                await _Dbcontext.Shifts.AddAsync(shift);
            }
                await _Dbcontext.SaveChangesAsync();
            return true;
        }
        public async Task<ShiftResponse> UpdateShift(UpdateShiftRequest request)
        {
            var shift = await _Dbcontext.Shifts.FindAsync(request.Date, request.PharmacyId);
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
                Count = shift.Count,
                Limit = shift.Limit,
                PharmacyId = shift.PharmacyId
            };
        }
        public async Task<List<Shift>> GetAll(DateOnly from, DateOnly to, int pharmacyId)
        {
            var result = await _Dbcontext.Shifts.Where(x => x.Date >= from && x.Date <= to && x.PharmacyId == pharmacyId).ToListAsync();

            return result;
        }
        public async Task<bool> DeleteShift(DeleteShiftRequest request)
        {
            var shift = await _Dbcontext.Shifts.FindAsync(request.Date, request.PharmacyId);
            if (shift == null)
            {
                return false;
            }
            _Dbcontext.Shifts.Remove(shift);
            await _Dbcontext.SaveChangesAsync();
            return true;
        }
        public async Task<PaginatedList<Shift>> getByPage(int pageIndex, int pageSize, int pharmacyId)
        {
            if(pageIndex < 1) pageIndex = 1;
            if (pageSize < 1) pageSize = 10;
            var total = await _Dbcontext.Shifts.Where(x => x.PharmacyId == pharmacyId).CountAsync();
            var result = await _Dbcontext.Shifts.Where(x => x.PharmacyId == pharmacyId)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return new PaginatedList<Shift>
            {
                Items = result,
                TotalItems = total,
                Page = pageIndex,
                PageSize = pageSize
            };
        }

    }
}
