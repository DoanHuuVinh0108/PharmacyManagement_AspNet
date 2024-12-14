using Microsoft.EntityFrameworkCore;
using PharmacyManagermentSystem.DbContext;
using PharmacyManagermentSystem.Model;
using PharmacyManagermentSystem.Request;
using PharmacyManagermentSystem.Response;

namespace PharmacyManagermentSystem.Services.MiniServiceReceipt
{
    public class ReceiptService : IReceiptService
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
        public async Task<PaginatedList<ReceiptResponse>> GetAll(int pageIndex, int pageSize, int pharmacyId)
        {
            if (pageIndex < 1) pageIndex = 1;
            if (pageSize < 1) pageSize = 10;

            var receipts = await _Dbcontext.Receipts.Where(x => x.PharmacyId == pharmacyId)
                .OrderBy(x => x.Id)
                .Include(x => x.Pharmacy)
                .Include(x => x.Employee)
                .Include(x => x.Supplier)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new ReceiptResponse
                {
                    Id = x.Id,
                    Date = x.Date,
                    SupplierId = x.SupplierId,
                    SupplierName = x.Supplier.Name,
                    PharmacyId = x.PharmacyId,
                    NamePharmacy = x.Pharmacy.Name,
                    EmployeeId = x.EmployeeId,
                    EmployeeName = x.Employee.FullName
                }).ToListAsync();
            var totalItems = await _Dbcontext.Receipts.Where(x => x.PharmacyId == pharmacyId).CountAsync();
            return new PaginatedList<ReceiptResponse>
            {
                Items = receipts,
                TotalItems = totalItems,
                Page = pageIndex,
                PageSize = pageSize
            };
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
        public async Task<bool> AddReceipt(ReceiptRequest request)
        {
            using (var transaction = await _Dbcontext.Database.BeginTransactionAsync())
            {
                try
                {
                    var receipt = new Receipt
                    {
                        Date = DateOnly.FromDateTime(DateTime.Now),
                        SupplierId = request.SupplierId,
                        PharmacyId = request.PharmacyId,
                        EmployeeId = request.EmployeeId
                    };
                    await _Dbcontext.Receipts.AddAsync(receipt);
                    await _Dbcontext.SaveChangesAsync();

                    var receiptDetails = new List<ReceiptDetail>();
                    var medicines = new List<Medicine>();

                    foreach (var category in request.Categories)
                    {
                        foreach (var lot in category.Lots)
                        {
                            foreach (var product in lot.Products)
                            {

                                var receiptDetail = new ReceiptDetail
                                {
                                    ReceiptId = receipt.Id,
                                    MedicineId = product.Id,
                                    BatchNumber = lot.Id,
                                    CategoryId = category.Id,
                                    Quantity = category.Quantity,
                                    Status = category.Status,
                                    Price = category.Price
                                };
                                receiptDetails.Add(receiptDetail);
                                var medicine = new Medicine
                                {
                                    Id = product.Id,
                                    BatchNumber = lot.Id,
                                    CategoryId = category.Id,
                                    Quantity = category.Quantity,
                                    Status = category.Status,
                                    ManufacturingDate = lot.ManufacturingDate,
                                    ExpiryDate = lot.ExpiryDate,
                                    PharmacyId = request.PharmacyId
                                };
                                medicines.Add(medicine);
                            }
                        }
                    }

                    await _Dbcontext.ReceiptDetails.AddRangeAsync(receiptDetails);
                    await _Dbcontext.Medicines.AddRangeAsync(medicines);
                    await _Dbcontext.SaveChangesAsync();

                    await transaction.CommitAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    Console.WriteLine($"Error adding receipt: {ex.Message}");
                    return false;
                }
            }
        }
        //public async Task<ReceiptDetailResponseById> getById(int id)
        //{
        //    var receiptDetailsQuery = _Dbcontext.Receipts
        //        .Where(x => x.Id == id)
        //        .Include(x => x.Pharmacy)
        //        .Include(x => x.Employee)
        //        .Include(x => x.Supplier)
        //        .Select(r => new
        //        {
        //            Receipt = r,
        //            ReceiptDetails = r.ReceiptDetails.Select(rd => new
        //            {
        //                rd.CategoryId,
        //                rd.MedicineId,
        //                rd.Status,
        //                rd.Quantity,
        //                rd.Price,
        //                rd.BatchNumber,
        //                rd.Category.MedicineName
        //            }).ToList()
        //        });

        //    var receiptData = await receiptDetailsQuery.FirstOrDefaultAsync();

        //    if (receiptData == null)
        //    {
        //        throw new Exception("Receipt not found");
        //    }

        //    var lotQuery = _Dbcontext.Medicines
        //        .Where(m => receiptData.ReceiptDetails.Select(rd => rd.BatchNumber).Contains(m.BatchNumber))
        //        .Select(m => new
        //        {
        //            m.BatchNumber,
        //            m.ManufacturingDate,
        //            m.ExpiryDate,
        //            m.Id,
        //            m.CategoryId
        //        });

        //    var lots = await lotQuery.ToListAsync();

        //    var categories = receiptData.ReceiptDetails
        //        .GroupBy(rd => new { rd.CategoryId, rd.MedicineName })
        //        .Select(g => new CategoryByReceipt
        //        {
        //            Id = g.Key.CategoryId,
        //            MedicineName = g.Key.MedicineName,
        //            Status = g.First().Status,
        //            Quantity = g.Sum(x => x.Quantity),
        //            Price = g.First().Price,
        //            Lots = lots
        //                .Where(l => l.CategoryId == g.Key.CategoryId)
        //                .Select(l => new Lot
        //                {
        //                    Id = l.BatchNumber,
        //                    ManufacturingDate = l.ManufacturingDate,
        //                    ExpiryDate = l.ExpiryDate,
        //                    Products = _Dbcontext.Medicines
        //                        .Where(z => z.BatchNumber == l.BatchNumber)
        //                        .Select(z => new Product { Id = z.Id })
        //                        .ToList()
        //                }).ToList()
        //        }).ToList();

        //    return new ReceiptDetailResponseById
        //    {
        //        NamePharmacy = receiptData.Receipt.Pharmacy.Name,
        //        SupplierName = receiptData.Receipt.Supplier.Name,
        //        EmployeeName = receiptData.Receipt.Employee.FullName,
        //        Date = receiptData.Receipt.Date,
        //        Categories = categories
        //    };
        //}
        //public async Task<ReceiptDetailResponseById> getById(int id)
        //{
        //    var receiptData = await _Dbcontext.Receipts
        //        .Where(x => x.Id == id)
        //        .Include(x => x.Pharmacy)
        //        .Include(x => x.Employee)
        //        .Include(x => x.Supplier)
        //        .Select(r => new
        //        {
        //            Receipt = r,
        //            ReceiptDetails = r.ReceiptDetails.Select(rd => new
        //            {
        //                rd.CategoryId,
        //                rd.Status,
        //                rd.Quantity,
        //                rd.Price,
        //                rd.BatchNumber,
        //                CategoryName = rd.Category.MedicineName,
                        
        //            }).ToList()

        //        })
        //        .FirstOrDefaultAsync();
        //    var categories = receiptData.ReceiptDetails
        //        .GroupBy(rd => new { rd.CategoryId, rd.CategoryName })
        //        .Select(g => new CategoryByReceipt
        //        {
        //            Id = g.Key.CategoryId,
        //            MedicineName = g.Key.CategoryName,
        //            Status = g.First().Status,
        //            Quantity = g.Sum(x => x.Quantity),
        //            Price = g.First().Price,
        //            Lots = g.Select(l => new Lot
        //            {
        //                Id = l.BatchNumber,
        //                Products = _Dbcontext.Medicines
        //                    .Where(z => z.BatchNumber == l.BatchNumber && z.CategoryId==l.CategoryId)
        //                    .Select(z => new Product { Id = z.Id })
        //                    .ToList()
        //            }).ToList()
        //        }).ToList();



        //    return new ReceiptDetailResponseById
        //    {
        //        NamePharmacy = receiptData.Receipt.Pharmacy.Name,
        //        SupplierName = receiptData.Receipt.Supplier.Name,
        //        EmployeeName = receiptData.Receipt.Employee.FullName,
        //        Date = receiptData.Receipt.Date,
        //        Categories = categories
        //    };
        //}
        public async Task<ReceiptDetailResponseById> getById(int id)
        {
            var receiptData = await _Dbcontext.Receipts
                .Where(x => x.Id == id)
                .Include(x => x.Pharmacy)
                .Include(x => x.Employee)
                .Include(x => x.Supplier)
                .Select(r => new
                {
                    Receipt = r,
                    ReceiptDetails = r.ReceiptDetails.Select(rd => new
                    {
                        rd.CategoryId,
                        rd.Status,
                        rd.Quantity,
                        rd.Price,
                        rd.BatchNumber,
                        rd.MedicineId,
                        
                        CategoryName = rd.Category.MedicineName,
                        

                    }).ToList()

                })
                .FirstOrDefaultAsync();
            var categories = receiptData.ReceiptDetails
            .GroupBy(x => new { CategoryId = x.CategoryId, CategoryName = x.CategoryName })
            .Select(group => new CategoryByReceipt
            {
                Id = group.Key.CategoryId.ToString(),
                MedicineName = group.Key.CategoryName.ToString(),
                Status = group.First().Status,
                Quantity = group.Sum(x => (int)x.Quantity),
                Price = group.First().Price,
                Lots = group
                    .GroupBy(x => x.BatchNumber)
                    .Select(lotGroup => new Lot
                    {
                        Id = lotGroup.Key.ToString(),
                        ManufacturingDate = DateOnly.FromDateTime(DateTime.Now), // You might want to replace this with actual data
                        ExpiryDate = DateOnly.FromDateTime(DateTime.Now.AddYears(1)), // You might want to replace this with actual data
                        Products = lotGroup.Select(product => new Product
                        {
                            Id = product.MedicineId.ToString()
                        }).ToList()
                    }).ToList()
            })
            .ToList();

            foreach (var category in categories)
            {
                foreach (var lot in category.Lots)
                {
                    var data = _Dbcontext.Medicines
                        .Where(m => m.BatchNumber == lot.Id && m.CategoryId == category.Id)
                        .Select(m => new { m.ManufacturingDate, m.ExpiryDate })
                        .FirstOrDefault();
                    if (data == null)
                    {
                        continue;
                    }
                    lot.ManufacturingDate = data.ManufacturingDate ;
                    lot.ExpiryDate = data.ExpiryDate;

                }
            }


            return new ReceiptDetailResponseById
            {
                NamePharmacy = receiptData.Receipt.Pharmacy.Name,
                SupplierName = receiptData.Receipt.Supplier.Name,
                EmployeeName = receiptData.Receipt.Employee.FullName,
                Date = receiptData.Receipt.Date,
                Categories = categories
            };
        }


    }



}


