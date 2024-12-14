using Microsoft.EntityFrameworkCore;
using PharmacyManagermentSystem.DbContext;
using PharmacyManagermentSystem.Model;
using PharmacyManagermentSystem.Request;
using PharmacyManagermentSystem.Response;

namespace PharmacyManagermentSystem.Services.MiniServiceSupplier
{
    public class SupplierService : ISupplierService
    {
        private readonly MyDbContext _Dbcontext;
        public SupplierService(MyDbContext Dbcontext)
        {
            _Dbcontext = Dbcontext;
        }
        public async Task<SupplierResponse> CreateSupplier(CreateSupplierRequest request)
        {
            var supplier = new Supplier
            {
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone,
                Address = request.Address
            };
            _Dbcontext.Suppliers.Add(supplier);
            await _Dbcontext.SaveChangesAsync();
            return new SupplierResponse
            {
                Id = supplier.Id,
                Name = supplier.Name,
                Email = supplier.Email,
                Phone = supplier.Phone,
                Address = supplier.Address
            };
        }
        public async Task<SupplierResponse> UpdateSupplier(int id, UpdateSupplierRequest request)
        {
            var supplier = await _Dbcontext.Suppliers.FindAsync(id);
            if (supplier == null)
            {
                throw new Exception("Supplier not found");
            }
            supplier.Name = request.Name;
            supplier.Email = request.Email;
            supplier.Phone = request.Phone;
            supplier.Address = request.Address;
            await _Dbcontext.SaveChangesAsync();
            return new SupplierResponse
            {
                Id = supplier.Id,
                Name = supplier.Name,
                Email = supplier.Email,
                Phone = supplier.Phone,
                Address = supplier.Address
            };
        }
        public async Task<List<Supplier>> GetAll()
        {
           return await _Dbcontext.Suppliers.ToListAsync();
        }
        public async Task<PaginatedList<Supplier>> GetByPage(int pageIndex, int pageSize)
        {
            if (pageIndex < 1) pageIndex = 1;
            if (pageSize < 1) pageSize = 10;
            var totalItems = await _Dbcontext.Suppliers.CountAsync();
            var suppliers = await _Dbcontext.Suppliers
                                             .Skip((pageIndex - 1) * pageSize)
                                             .Take(pageSize)
                                             .ToListAsync();
            return new PaginatedList<Supplier>
            {
                Items = suppliers,
                TotalItems = totalItems,
                Page = pageIndex,
                PageSize = pageSize
            };
        }
        public async Task<bool> DeleteSupplier(int id)
        {
            var supplier = await _Dbcontext.Suppliers.FindAsync(id);
            if (supplier == null)
            {
                throw new Exception("Supplier not found");
            }
            _Dbcontext.Suppliers.Remove(supplier);
            await _Dbcontext.SaveChangesAsync();
            return true;
        }
    }
}
