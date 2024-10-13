using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using PharmacyManagermentSystem.DbContext;
using PharmacyManagermentSystem.Model;
using PharmacyManagermentSystem.Request;
using PharmacyManagermentSystem.Response;

namespace PharmacyManagermentSystem.Services.MiniServiceReceiptDetail
{
    public class ReceiptDetailService :IReceiptDetailService
    {
        private readonly MyDbContext _Dbcontext;
        public ReceiptDetailService(MyDbContext context)
        {
            _Dbcontext = context;
        }
        public async Task<ReceiptDetailResponse> CreateReceiptDetail(CreateReceiptDetailRequest request)
        {
            var receiptDetail = new ReceiptDetail
            {
                ReceiptId = request.ReceiptId,
                MedicineId = request.MedicineId,
                BatchNumber = request.BatchNumber,
                CategoryId = request.CategoryId,
                Quantity = request.Quantity,
                Status = request.Status,
                Price = request.Price
            };
            await _Dbcontext.ReceiptDetails.AddAsync(receiptDetail);
            await _Dbcontext.SaveChangesAsync();
            return new ReceiptDetailResponse
            {
                ReceiptId = receiptDetail.ReceiptId,
                MedicineId = receiptDetail.MedicineId,
                BatchNumber = receiptDetail.BatchNumber,
                CategoryId = receiptDetail.CategoryId,
                Quantity = receiptDetail.Quantity,
                Status = receiptDetail.Status,
                Price = receiptDetail.Price
            };
        }
        public async Task<ReceiptDetailResponse> UpdateReceiptDetail(UpdateReceiptDetailRequest request)
        {
            var receiptDetail = await _Dbcontext.ReceiptDetails.FirstOrDefaultAsync(x =>
            x.ReceiptId == request.ReceiptId && x.MedicineId == request.MedicineId && x.BatchNumber == request.BatchNumber && x.CategoryId == request.CategoryId);
            if (receiptDetail == null)
            {
                throw new Exception("ReceiptDetail not found");
            }
            receiptDetail.Quantity = request.Quantity;
            receiptDetail.Status = request.Status;
            receiptDetail.Price = request.Price;
            receiptDetail.CategoryId = request.NewCategoryId == null ? receiptDetail.CategoryId : request.NewCategoryId;
            _Dbcontext.ReceiptDetails.Update(receiptDetail);
            await _Dbcontext.SaveChangesAsync();
            return new ReceiptDetailResponse
            {
                ReceiptId = receiptDetail.ReceiptId,
                MedicineId = receiptDetail.MedicineId,
                BatchNumber = receiptDetail.BatchNumber,
                CategoryId = receiptDetail.CategoryId,
                Quantity = receiptDetail.Quantity,
                Status = receiptDetail.Status,
                Price = receiptDetail.Price
            };
        }
        public async Task<List<ReceiptDetail>> GetAll()
        { 
            return await _Dbcontext.ReceiptDetails.AsNoTracking().ToListAsync();
        }
        public async Task<bool> DeleteReceiptDetail(DeleteReceiptDetailRequest request)
        {
            var receiptDetail = await _Dbcontext.ReceiptDetails.FirstOrDefaultAsync(x =>
            x.ReceiptId == request.ReceiptId && x.MedicineId == request.MedicineId && x.BatchNumber == request.BatchNumber && x.CategoryId == request.CategoryId);
            if (receiptDetail == null)
            {
                throw new Exception("ReceiptDetail not found");
            }
            _Dbcontext.ReceiptDetails.Remove(receiptDetail);
            await _Dbcontext.SaveChangesAsync();
            return true;
        }

    }
}
