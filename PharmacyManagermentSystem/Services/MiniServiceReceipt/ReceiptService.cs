using Microsoft.EntityFrameworkCore;
using PharmacyManagermentSystem.DbContext;
using PharmacyManagermentSystem.Model;
using PharmacyManagermentSystem.Request;
using PharmacyManagermentSystem.Response;

namespace PharmacyManagermentSystem.Services.MiniServiceReceipt
{
    public class ReceiptService: IReceiptService
    {
        private readonly MyDbContext _Dbcontext;
        public ReceiptService(MyDbContext Dbcontext)
        {
            _Dbcontext = Dbcontext;
        }
        public async Task<ReceiptResponse> CreateReceipt(CreateReceiptRequest request) 
        {
            var receipt = new Receipt
            {
                Date = request.Date,
                SupplierId = request.SupplierId,
                PharmacyId = request.PharmacyId,
                EmployeeId = request.EmployeeId
            };
            await _Dbcontext.Receipts.AddAsync(receipt);
            await _Dbcontext.SaveChangesAsync();
            return new ReceiptResponse
            {
                Id = receipt.Id,
                Date = receipt.Date,
                SupplierId = receipt.SupplierId,
                PharmacyId = receipt.PharmacyId,
                EmployeeId = receipt.EmployeeId
            };
        }    
        public async Task<ReceiptResponse> UpdateReceipt(int id, UpdateReceiptRequest request)
        {
            var receipt = await _Dbcontext.Receipts.FindAsync(id);
            if (receipt == null)
            {
                throw new Exception("Receipt not found");
            }
            receipt.Date = request.Date;
            receipt.SupplierId = request.SupplierId;
            receipt.PharmacyId = request.PharmacyId;
            receipt.EmployeeId = request.EmployeeId;
            await _Dbcontext.SaveChangesAsync();
            return new ReceiptResponse
            {
                Id = receipt.Id,
                Date = receipt.Date,
                SupplierId = receipt.SupplierId,
                PharmacyId = receipt.PharmacyId,
                EmployeeId = receipt.EmployeeId
            };
        }
        public async Task<List<Receipt>> GetAll()
        {
           return await _Dbcontext.Receipts.ToListAsync();
        }
        public async Task<bool> Delete(int id)
        {
            var receipt = await _Dbcontext.Receipts.FindAsync(id);
            if (receipt == null)
            {
                throw new Exception("Receipt not found");
            }
            _Dbcontext.Receipts.Remove(receipt);
            await _Dbcontext.SaveChangesAsync();
            return true;
        }       
    }
}
