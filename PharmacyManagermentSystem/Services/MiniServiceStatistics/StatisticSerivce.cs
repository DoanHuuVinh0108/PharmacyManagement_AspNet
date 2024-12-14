using Microsoft.EntityFrameworkCore;
using PharmacyManagermentSystem.DbContext;
using PharmacyManagermentSystem.Model;
using PharmacyManagermentSystem.Response;

namespace PharmacyManagermentSystem.Services.MiniServiceStatistics
{
    public class StatisticSerivce : IStatisticService
    {
        private readonly MyDbContext _dbContext;
        private readonly IServiceProvider _serviceProvider;
        public StatisticSerivce(MyDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _serviceProvider = serviceProvider;

        }

        public async Task<StatisticsResponse> GetProfitAllPharmacy(int month, int year)
        {
            var result = new StatisticsResponse
            {
                RevenueList = new ListRevenueResponse
                {
                    OrderRevenue = new List<RevenueResponse>(),
                    ReturnSupplierRevenue = new List<RevenueResponse>()
                },
                ExpenseList = new ListExpenseResponse
                {
                    SalaryExpense = new List<ExpenseResponse>(),
                    ReceiptExpense = new List<ExpenseResponse>()
                }
            };

            var tasks = new List<Task>();

            // Fetch order data
            var priceOrderTask = Task.Run(async () =>
            {
                using var scope = _serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();

                result.RevenueList.OrderRevenue = await dbContext.Orders
                    .AsNoTracking()
                    .Where(x => x.Date.Month == month && x.Date.Year == year )
                    .GroupBy(x => x.Date.Day)
                    .Select(group => new RevenueResponse
                    {
                        Price = group.Sum(x => x.TotalPrice),
                        Unit = group.Key,
                    })
                    .OrderBy(x => x.Unit)
                    .ToListAsync();

                result.RevenueList.TotalOrder = result.RevenueList.OrderRevenue.Sum(x => x.Price);
            });

            tasks.Add(priceOrderTask);

            // Fetch return supplier data
            var priceReturnSupplierTask = Task.Run(async () =>
            {
                using var scope = _serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();

                result.RevenueList.ReturnSupplierRevenue = await dbContext.ReturnSuppliers
                    .AsNoTracking()
                    .Where(x => x.Date.Month == month && x.Date.Year == year )
                    .GroupBy(x => x.Date.Day)
                    .Select(group => new RevenueResponse
                    {
                        Price = group.Sum(x => x.Price * x.Quantity),
                        Unit = group.Key,
                    })
                    .OrderBy(x => x.Unit)
                    .ToListAsync();

                result.RevenueList.TotalReturnSupplier = result.RevenueList.ReturnSupplierRevenue.Sum(x => x.Price);
            });

            tasks.Add(priceReturnSupplierTask);

            // Fetch salary data
            var priceSalaryTask = Task.Run(async () =>
            {
                using var scope = _serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();

                result.ExpenseList.SalaryExpense = await dbContext.Salarys
                    .AsNoTracking()
                    .Where(x => x.Month == month && x.Year == year )
                    .GroupBy(x => x.Month)
                    .Select(group => new ExpenseResponse
                    {
                        Price = group.Sum(x => x.BasicSalary + x.Bonus),
                        Unit = group.Key,
                    })
                    .OrderBy(x => x.Unit)
                    .ToListAsync();

                result.ExpenseList.TotalSalary = result.ExpenseList.SalaryExpense.Sum(x => x.Price);
            });

            tasks.Add(priceSalaryTask);

            // Fetch receipt data
            var priceReceiptTask = Task.Run(async () =>
            {
                using var scope = _serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();

                result.ExpenseList.ReceiptExpense = await dbContext.ReceiptDetails
                    .AsNoTracking()
                    .Where(x => x.Receipt.Date.Month == month && x.Receipt.Date.Year == year )
                    .GroupBy(x => x.Receipt.Date.Day)
                    .Select(group => new ExpenseResponse
                    {
                        Price = group.Sum(x => x.Price * x.Quantity),
                        Unit = group.Key,
                    })
                    .OrderBy(x => x.Unit)
                    .ToListAsync();

                result.ExpenseList.TotalReceipt = result.ExpenseList.ReceiptExpense.Sum(x => x.Price);
            });

            tasks.Add(priceReceiptTask);

            // Wait for all tasks to complete
            await Task.WhenAll(tasks);

            // Calculate total revenue, expense, and profit
            result.RevenueList.TotalRevenue = result.RevenueList.TotalOrder + result.RevenueList.TotalReturnSupplier;
            result.ExpenseList.TotalExpense = result.ExpenseList.TotalSalary + result.ExpenseList.TotalReceipt;
            result.TotalProfit = result.RevenueList.TotalRevenue - result.ExpenseList.TotalExpense;

            return result;
        }
        public async Task<StatisticsResponse> GetProfitWithPharmacy(int month, int year, int pharmacyId)
        {
            var result = new StatisticsResponse
            {
                RevenueList = new ListRevenueResponse
                {
                    OrderRevenue = new List<RevenueResponse>(),
                    ReturnSupplierRevenue = new List<RevenueResponse>()
                },
                ExpenseList = new ListExpenseResponse
                {
                    SalaryExpense = new List<ExpenseResponse>(),
                    ReceiptExpense = new List<ExpenseResponse>()
                }
            };

            var tasks = new List<Task>();

            // Fetch order data
            var priceOrderTask = Task.Run(async () =>
            {
                using var scope = _serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();

                result.RevenueList.OrderRevenue = await dbContext.Orders
                    .AsNoTracking()
                    .Where(x => x.Date.Month == month && x.Date.Year == year && x.PharmacyId == pharmacyId)
                    .GroupBy(x => x.Date.Day)
                    .Select(group => new RevenueResponse
                    {
                        Price = group.Sum(x => x.TotalPrice),
                        Unit = group.Key,
                    })
                    .OrderBy(x => x.Unit)
                    .ToListAsync();

                result.RevenueList.TotalOrder = result.RevenueList.OrderRevenue.Sum(x => x.Price);
            });

            tasks.Add(priceOrderTask);

            // Fetch return supplier data
            var priceReturnSupplierTask = Task.Run(async () =>
            {
                using var scope = _serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();

                result.RevenueList.ReturnSupplierRevenue = await dbContext.ReturnSuppliers
                    .AsNoTracking()
                    .Where(x => x.Date.Month == month && x.Date.Year == year && x.Medicine.PharmacyId == pharmacyId)
                    .GroupBy(x => x.Date.Day)
                    .Select(group => new RevenueResponse
                    {
                        Price = group.Sum(x => x.Price * x.Quantity),
                        Unit = group.Key,
                    })
                    .OrderBy(x => x.Unit)
                    .ToListAsync();

                result.RevenueList.TotalReturnSupplier = result.RevenueList.ReturnSupplierRevenue.Sum(x => x.Price);
            });

            tasks.Add(priceReturnSupplierTask);

            // Fetch salary data
            var priceSalaryTask = Task.Run(async () =>
            {
                using var scope = _serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();

                result.ExpenseList.SalaryExpense = await dbContext.Salarys
                    .AsNoTracking()
                    .Where(x => x.Month == month && x.Year == year && x.Employee.PharmacyId == pharmacyId)
                    .GroupBy(x => x.Month)
                    .Select(group => new ExpenseResponse
                    {
                        Price = group.Sum(x => x.BasicSalary + x.Bonus),
                        Unit = group.Key,
                    })
                    .OrderBy(x => x.Unit)
                    .ToListAsync();

                result.ExpenseList.TotalSalary = result.ExpenseList.SalaryExpense.Sum(x => x.Price);
            });

            tasks.Add(priceSalaryTask);

            // Fetch receipt data
            var priceReceiptTask = Task.Run(async () =>
            {
                using var scope = _serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();

                result.ExpenseList.ReceiptExpense = await dbContext.ReceiptDetails
                    .AsNoTracking()
                    .Where(x => x.Receipt.Date.Month == month && x.Receipt.Date.Year == year && x.Receipt.PharmacyId == pharmacyId)
                    .GroupBy(x => x.Receipt.Date.Day)
                    .Select(group => new ExpenseResponse
                    {
                        Price = group.Sum(x => x.Price * x.Quantity),
                        Unit = group.Key,
                    })
                    .OrderBy(x => x.Unit)
                    .ToListAsync();

                result.ExpenseList.TotalReceipt = result.ExpenseList.ReceiptExpense.Sum(x => x.Price);
            });

            tasks.Add(priceReceiptTask);

            // Wait for all tasks to complete
            await Task.WhenAll(tasks);

            // Calculate total revenue, expense, and profit
            result.RevenueList.TotalRevenue = result.RevenueList.TotalOrder + result.RevenueList.TotalReturnSupplier;
            result.ExpenseList.TotalExpense = result.ExpenseList.TotalSalary + result.ExpenseList.TotalReceipt;
            result.TotalProfit = result.RevenueList.TotalRevenue - result.ExpenseList.TotalExpense;

            return result;
        }
        public async Task<StatisticsResponse> GetProfitAllPharmacy(int year)
        {
            var result = new StatisticsResponse
            {
                RevenueList = new ListRevenueResponse
                {
                    OrderRevenue = new List<RevenueResponse>(),
                    ReturnSupplierRevenue = new List<RevenueResponse>()
                },
                ExpenseList = new ListExpenseResponse
                {
                    SalaryExpense = new List<ExpenseResponse>(),
                    ReceiptExpense = new List<ExpenseResponse>()
                }
            };

            var tasks = new List<Task>();

            // Fetch order data
            var priceOrderTask = Task.Run(async () =>
            {
                using var scope = _serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();

                result.RevenueList.OrderRevenue = await dbContext.Orders
                    .AsNoTracking()
                    .Where(x => x.Date.Year == year)
                    .GroupBy(x => x.Date.Month)
                    .Select(group => new RevenueResponse
                    {
                        Price = group.Sum(x => x.TotalPrice),
                        Unit = group.Key,
                    })
                    .OrderBy(x => x.Unit)
                    .ToListAsync();

                result.RevenueList.TotalOrder = result.RevenueList.OrderRevenue.Sum(x => x.Price);
            });

            tasks.Add(priceOrderTask);

            // Fetch return supplier data
            var priceReturnSupplierTask = Task.Run(async () =>
            {
                using var scope = _serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();

                result.RevenueList.ReturnSupplierRevenue = await dbContext.ReturnSuppliers
                    .AsNoTracking()
                    .Where(x => x.Date.Year == year)
                    .GroupBy(x => x.Date.Month)
                    .Select(group => new RevenueResponse
                    {
                        Price = group.Sum(x => x.Price * x.Quantity),
                        Unit = group.Key,
                    })
                    .OrderBy(x => x.Unit)
                    .ToListAsync();

                result.RevenueList.TotalReturnSupplier = result.RevenueList.ReturnSupplierRevenue.Sum(x => x.Price);
            });

            tasks.Add(priceReturnSupplierTask);

            // Fetch salary data
            var priceSalaryTask = Task.Run(async () =>
            {
                using var scope = _serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();

                result.ExpenseList.SalaryExpense = await dbContext.Salarys
                    .AsNoTracking()
                    .Where(x => x.Year == year)
                    .GroupBy(x => x.Month)
                    .Select(group => new ExpenseResponse
                    {
                        Price = group.Sum(x => x.BasicSalary + x.Bonus),
                        Unit = group.Key,
                    })
                    .OrderBy(x => x.Unit)
                    .ToListAsync();

                result.ExpenseList.TotalSalary = result.ExpenseList.SalaryExpense.Sum(x => x.Price);
            });

            tasks.Add(priceSalaryTask);

            // Fetch receipt data
            var priceReceiptTask = Task.Run(async () =>
            {
                using var scope = _serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();

                result.ExpenseList.ReceiptExpense = await dbContext.ReceiptDetails
                    .AsNoTracking()
                    .Where(x => x.Receipt.Date.Year == year)
                    .GroupBy(x => x.Receipt.Date.Month)
                    .Select(group => new ExpenseResponse
                    {
                        Price = group.Sum(x => x.Price * x.Quantity),
                        Unit = group.Key,
                    })
                    .OrderBy(x => x.Unit)
                    .ToListAsync();

                result.ExpenseList.TotalReceipt = result.ExpenseList.ReceiptExpense.Sum(x => x.Price);
            });

            tasks.Add(priceReceiptTask);

            // Wait for all tasks to complete
            await Task.WhenAll(tasks);

            // Calculate total revenue, expense, and profit
            result.RevenueList.TotalRevenue = result.RevenueList.TotalOrder + result.RevenueList.TotalReturnSupplier;
            result.ExpenseList.TotalExpense = result.ExpenseList.TotalSalary + result.ExpenseList.TotalReceipt;
            result.TotalProfit = result.RevenueList.TotalRevenue - result.ExpenseList.TotalExpense;

            return result;
        }
        public async Task<StatisticsResponse> GetProfitWithPharmacy(int year, int pharmacyId)
        {
            var result = new StatisticsResponse
            {
                RevenueList = new ListRevenueResponse
                {
                    OrderRevenue = new List<RevenueResponse>(),
                    ReturnSupplierRevenue = new List<RevenueResponse>()
                },
                ExpenseList = new ListExpenseResponse
                {
                    SalaryExpense = new List<ExpenseResponse>(),
                    ReceiptExpense = new List<ExpenseResponse>()
                }
            };

            var tasks = new List<Task>();

            // Fetch order data
            var priceOrderTask = Task.Run(async () =>
            {
                using var scope = _serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();

                result.RevenueList.OrderRevenue = await dbContext.Orders
                    .AsNoTracking()
                    .Where(x => x.Date.Year == year && x.PharmacyId == pharmacyId)
                    .GroupBy(x => x.Date.Month)
                    .Select(group => new RevenueResponse
                    {
                        Price = group.Sum(x => x.TotalPrice),
                        Unit = group.Key,
                    })
                    .OrderBy(x => x.Unit)
                    .ToListAsync();

                result.RevenueList.TotalOrder = result.RevenueList.OrderRevenue.Sum(x => x.Price);
            });

            tasks.Add(priceOrderTask);

            // Fetch return supplier data
            var priceReturnSupplierTask = Task.Run(async () =>
            {
                using var scope = _serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();

                result.RevenueList.ReturnSupplierRevenue = await dbContext.ReturnSuppliers
                    .AsNoTracking()
                    .Where(x => x.Date.Year == year && x.Medicine.PharmacyId == pharmacyId)
                    .GroupBy(x => x.Date.Month)
                    .Select(group => new RevenueResponse
                    {
                        Price = group.Sum(x => x.Price * x.Quantity),
                        Unit = group.Key,
                    })
                    .OrderBy(x => x.Unit)
                    .ToListAsync();

                result.RevenueList.TotalReturnSupplier = result.RevenueList.ReturnSupplierRevenue.Sum(x => x.Price);
            });

            tasks.Add(priceReturnSupplierTask);

            // Fetch salary data
            var priceSalaryTask = Task.Run(async () =>
            {
                using var scope = _serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();

                result.ExpenseList.SalaryExpense = await dbContext.Salarys
                    .AsNoTracking()
                    .Where(x => x.Year == year && x.Employee.PharmacyId == pharmacyId)
                    .GroupBy(x => x.Month)
                    .Select(group => new ExpenseResponse
                    {
                        Price = group.Sum(x => x.BasicSalary + x.Bonus),
                        Unit = group.Key,
                    })
                    .OrderBy(x => x.Unit)
                    .ToListAsync();

                result.ExpenseList.TotalSalary = result.ExpenseList.SalaryExpense.Sum(x => x.Price);
            });

            tasks.Add(priceSalaryTask);

            // Fetch receipt data
            var priceReceiptTask = Task.Run(async () =>
            {
                using var scope = _serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();

                result.ExpenseList.ReceiptExpense = await dbContext.ReceiptDetails
                    .AsNoTracking()
                    .Where(x =>x.Receipt.Date.Year == year && x.Receipt.PharmacyId == pharmacyId)
                    .GroupBy(x => x.Receipt.Date.Month)
                    .Select(group => new ExpenseResponse
                    {
                        Price = group.Sum(x => x.Price * x.Quantity),
                        Unit = group.Key,
                    })
                    .OrderBy(x => x.Unit)
                    .ToListAsync();

                result.ExpenseList.TotalReceipt = result.ExpenseList.ReceiptExpense.Sum(x => x.Price);
            });

            tasks.Add(priceReceiptTask);

            // Wait for all tasks to complete
            await Task.WhenAll(tasks);

            // Calculate total revenue, expense, and profit
            result.RevenueList.TotalRevenue = result.RevenueList.TotalOrder + result.RevenueList.TotalReturnSupplier;
            result.ExpenseList.TotalExpense = result.ExpenseList.TotalSalary + result.ExpenseList.TotalReceipt;
            result.TotalProfit = result.RevenueList.TotalRevenue - result.ExpenseList.TotalExpense;

            return result;
        }
        public async Task<StatisticsResponse> GetProfitAllPharmacy (DateOnly fromDate, DateOnly toDate)
        {
            var result = new StatisticsResponse
            {
                RevenueList = new ListRevenueResponse
                {
                    OrderRevenue = new List<RevenueResponse>(),
                    ReturnSupplierRevenue = new List<RevenueResponse>()
                },
                ExpenseList = new ListExpenseResponse
                {
                    SalaryExpense = new List<ExpenseResponse>(),
                    ReceiptExpense = new List<ExpenseResponse>()
                }
            };

            var tasks = new List<Task>();

            // Fetch order data
            var priceOrderTask = Task.Run(async () =>
            {
                using var scope = _serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();

                result.RevenueList.OrderRevenue = await dbContext.Orders
                    .AsNoTracking()
                    .Where(x => x.Date >= fromDate && x.Date <= toDate)
                    .GroupBy(x => x.Date.Day)
                    .Select(group => new RevenueResponse
                    {
                        Price = group.Sum(x => x.TotalPrice),
                        Unit = group.Key,
                    })
                    .OrderBy(x => x.Unit)
                    .ToListAsync();

                result.RevenueList.TotalOrder = result.RevenueList.OrderRevenue.Sum(x => x.Price);
            });

            tasks.Add(priceOrderTask);

            // Fetch return supplier data
            var priceReturnSupplierTask = Task.Run(async () =>
            {
                using var scope = _serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();

                result.RevenueList.ReturnSupplierRevenue = await dbContext.ReturnSuppliers
                    .AsNoTracking()
                    .Where(x => x.Date >= fromDate && x.Date <= toDate)
                    .GroupBy(x => x.Date.Day)
                    .Select(group => new RevenueResponse
                    {
                        Price = group.Sum(x => x.Price * x.Quantity),
                        Unit = group.Key,
                    })
                    .OrderBy(x => x.Unit)
                    .ToListAsync();

                result.RevenueList.TotalReturnSupplier = result.RevenueList.ReturnSupplierRevenue.Sum(x => x.Price);
            });

            tasks.Add(priceReturnSupplierTask);

            // Fetch salary data
            var priceSalaryTask = Task.Run(async () =>
            {
                using var scope = _serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();

                result.ExpenseList.SalaryExpense = await dbContext.Salarys
                    .AsNoTracking()
                    .Where(x =>
            (x.Year > fromDate.Year || (x.Year == fromDate.Year && x.Month >= fromDate.Month)) &&
            (x.Year < toDate.Year || (x.Year == toDate.Year && x.Month <= toDate.Month)))
                    .GroupBy(x => x.Month)
                    .Select(group => new ExpenseResponse
                    {
                        Price = group.Sum(x => x.BasicSalary + x.Bonus),
                        Unit = group.Key,
                    })
                    .OrderBy(x => x.Unit)
                    .ToListAsync();

                result.ExpenseList.TotalSalary = result.ExpenseList.SalaryExpense.Sum(x => x.Price);
            });

            tasks.Add(priceSalaryTask);

            // Fetch receipt data
            var priceReceiptTask = Task.Run(async () =>
            {
                using var scope = _serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();

                result.ExpenseList.ReceiptExpense = await dbContext.ReceiptDetails
                    .AsNoTracking()
                    .Where(x => x.Receipt.Date >=fromDate && x.Receipt.Date <= toDate)
                    .GroupBy(x => x.Receipt.Date.Day)
                    .Select(group => new ExpenseResponse
                    {
                        Price = group.Sum(x => x.Price * x.Quantity),
                        Unit = group.Key,
                    })
                    .OrderBy(x => x.Unit)
                    .ToListAsync();

                result.ExpenseList.TotalReceipt = result.ExpenseList.ReceiptExpense.Sum(x => x.Price);
            });

            tasks.Add(priceReceiptTask);

            // Wait for all tasks to complete
            await Task.WhenAll(tasks);

            // Calculate total revenue, expense, and profit
            result.RevenueList.TotalRevenue = result.RevenueList.TotalOrder + result.RevenueList.TotalReturnSupplier;
            result.ExpenseList.TotalExpense = result.ExpenseList.TotalSalary + result.ExpenseList.TotalReceipt;
            result.TotalProfit = result.RevenueList.TotalRevenue - result.ExpenseList.TotalExpense;

            return result;
        }
        public async Task<StatisticsResponse> GetProfitWithPharmacy(DateOnly fromDate, DateOnly toDate,int pharmacyId)
        {
            var result = new StatisticsResponse
            {
                RevenueList = new ListRevenueResponse
                {
                    OrderRevenue = new List<RevenueResponse>(),
                    ReturnSupplierRevenue = new List<RevenueResponse>()
                },
                ExpenseList = new ListExpenseResponse
                {
                    SalaryExpense = new List<ExpenseResponse>(),
                    ReceiptExpense = new List<ExpenseResponse>()
                }
            };

            var tasks = new List<Task>();

            // Fetch order data
            var priceOrderTask = Task.Run(async () =>
            {
                using var scope = _serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();

                result.RevenueList.OrderRevenue = await dbContext.Orders
                    .AsNoTracking()
                    .Where(x => x.Date >= fromDate && x.Date <= toDate && x.PharmacyId==pharmacyId)
                    .GroupBy(x => x.Date.Day)
                    .Select(group => new RevenueResponse
                    {
                        Price = group.Sum(x => x.TotalPrice),
                        Unit = group.Key,
                    })
                    .OrderBy(x => x.Unit)
                    .ToListAsync();

                result.RevenueList.TotalOrder = result.RevenueList.OrderRevenue.Sum(x => x.Price);
            });

            tasks.Add(priceOrderTask);

            // Fetch return supplier data
            var priceReturnSupplierTask = Task.Run(async () =>
            {
                using var scope = _serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();

                result.RevenueList.ReturnSupplierRevenue = await dbContext.ReturnSuppliers
                    .AsNoTracking()
                    .Where(x => x.Date >= fromDate && x.Date <= toDate && x.Medicine.PharmacyId == pharmacyId)
                    .GroupBy(x => x.Date.Day)
                    .Select(group => new RevenueResponse
                    {
                        Price = group.Sum(x => x.Price * x.Quantity),
                        Unit = group.Key,
                    })
                    .OrderBy(x => x.Unit)
                    .ToListAsync();

                result.RevenueList.TotalReturnSupplier = result.RevenueList.ReturnSupplierRevenue.Sum(x => x.Price);
            });

            tasks.Add(priceReturnSupplierTask);

            // Fetch salary data
            var priceSalaryTask = Task.Run(async () =>
            {
                using var scope = _serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();

                result.ExpenseList.SalaryExpense = await dbContext.Salarys
                    .AsNoTracking()
                    .Where(x =>
            (x.Year > fromDate.Year || (x.Year == fromDate.Year && x.Month >= fromDate.Month)) &&
            (x.Year < toDate.Year || (x.Year == toDate.Year && x.Month <= toDate.Month))
            && x.Employee.PharmacyId ==pharmacyId) 
                    .GroupBy(x => x.Month)
                    .Select(group => new ExpenseResponse
                    {
                        Price = group.Sum(x => x.BasicSalary + x.Bonus),
                        Unit = group.Key,
                    })
                    .OrderBy(x => x.Unit)
                    .ToListAsync();

                result.ExpenseList.TotalSalary = result.ExpenseList.SalaryExpense.Sum(x => x.Price);
            });

            tasks.Add(priceSalaryTask);

            // Fetch receipt data
            var priceReceiptTask = Task.Run(async () =>
            {
                using var scope = _serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();

                result.ExpenseList.ReceiptExpense = await dbContext.ReceiptDetails
                    .AsNoTracking()
                    .Where(x => x.Receipt.Date >= fromDate && x.Receipt.Date <= toDate && x.Receipt.PharmacyId == pharmacyId)
                    .GroupBy(x => x.Receipt.Date.Day)
                    .Select(group => new ExpenseResponse
                    {
                        Price = group.Sum(x => x.Price * x.Quantity),
                        Unit = group.Key,
                    })
                    .OrderBy(x => x.Unit)
                    .ToListAsync();

                result.ExpenseList.TotalReceipt = result.ExpenseList.ReceiptExpense.Sum(x => x.Price);
            });

            tasks.Add(priceReceiptTask);

            // Wait for all tasks to complete
            await Task.WhenAll(tasks);

            // Calculate total revenue, expense, and profit
            result.RevenueList.TotalRevenue = result.RevenueList.TotalOrder + result.RevenueList.TotalReturnSupplier;
            result.ExpenseList.TotalExpense = result.ExpenseList.TotalSalary + result.ExpenseList.TotalReceipt;
            result.TotalProfit = result.RevenueList.TotalRevenue - result.ExpenseList.TotalExpense;

            return result;
        }
        public async Task<List<GetTopSellByAllPharmacyResponse>> GetTopSellByAllPharmacy(int month, int year)
        {
            return await _dbContext.OrderDetails.AsNoTracking()
                .Where(x => x.Order.Date.Month == month && x.Order.Date.Year == year)
                .GroupBy(x => x.CategoryId)
                .Select(group => new
                {
                    CategoryId = group.Key,
                    TotalQuantity = group.Sum(x => x.Quantity)
                })
                .Join(
                    _dbContext.Categorys,
                    grouped => grouped.CategoryId,
                    category => category.Id,
                    (grouped, category) => new GetTopSellByAllPharmacyResponse
                    {
                        Id = category.Id,
                        Quantity = grouped.TotalQuantity,
                        MedicineName = category.MedicineName,
                        Price = category.Price
                    })
                .OrderByDescending(x => x.Quantity)
                .Take(5)
                .ToListAsync();
        }
        public async Task<List<GetTopSellByAllPharmacyResponse>> GetTopSellByPharmacy(int month, int year, int pharmacyId)
        {
            return await _dbContext.OrderDetails.AsNoTracking()
                .Where(x => x.Order.Date.Month == month && x.Order.Date.Year == year && x.Order.PharmacyId == pharmacyId)
                .GroupBy(x => x.CategoryId)
                .Select(group => new
                {
                    CategoryId = group.Key,
                    TotalQuantity = group.Sum(x => x.Quantity)
                })
                .Join(
                    _dbContext.Categorys,
                    grouped => grouped.CategoryId,
                    category => category.Id,
                    (grouped, category) => new GetTopSellByAllPharmacyResponse
                    {
                        Id = category.Id,
                        Quantity = grouped.TotalQuantity,
                        MedicineName = category.MedicineName,
                        Price = category.Price
                    })
                .OrderByDescending(x => x.Quantity)
                .Take(5)
                .ToListAsync();
        }
        public async Task<StatisticsResponse> GetProfitProductWithPharmacy(int month,int year, int pharmacyId, string categoryId)
        {
            var result = new StatisticsResponse
            {
                RevenueList = new ListRevenueResponse
                {
                    OrderRevenue = new List<RevenueResponse>(),
                    ReturnSupplierRevenue = new List<RevenueResponse>()
                },
                ExpenseList = new ListExpenseResponse
                {
                    SalaryExpense = new List<ExpenseResponse>(),
                    ReceiptExpense = new List<ExpenseResponse>()
                }
            };

            var tasks = new List<Task>();

            // Fetch order data
            var priceOrderTask = Task.Run(async () =>
            {
                using var scope = _serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();

                result.RevenueList.OrderRevenue = await dbContext.Orders
                    .AsNoTracking()
                    .Include(x => x.OrderDetails)
                    .Where(x => x.Date.Month == month && x.Date.Year == year && x.PharmacyId == pharmacyId && x.OrderDetails.Any(x => x.CategoryId==categoryId))
                    .GroupBy(x => x.Date.Day)
                    .Select(group => new RevenueResponse
                    {
                        Price = group.SelectMany(x => x.OrderDetails).Sum(od => od.Quantity*od.Price),
                        Unit = group.Key,
                    })
                    .OrderBy(x => x.Unit)
                    .ToListAsync();

                result.RevenueList.TotalOrder = result.RevenueList.OrderRevenue.Sum(x => x.Price);
            });

            tasks.Add(priceOrderTask);

            // Fetch return supplier data
            var priceReturnSupplierTask = Task.Run(async () =>
            {
                using var scope = _serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();

                result.RevenueList.ReturnSupplierRevenue = await dbContext.ReturnSuppliers
                    .AsNoTracking()
                    .Where(x => x.Date.Month == month && x.Date.Year == year && x.Medicine.PharmacyId == pharmacyId && x.CategoryId == categoryId)
                    .GroupBy(x => x.Date.Day)
                    .Select(group => new RevenueResponse
                    {
                        Price = group.Sum(x => x.Price * x.Quantity),
                        Unit = group.Key,
                    })
                    .OrderBy(x => x.Unit)
                    .ToListAsync();

                result.RevenueList.TotalReturnSupplier = result.RevenueList.ReturnSupplierRevenue.Sum(x => x.Price);
            });

            tasks.Add(priceReturnSupplierTask);

            // Fetch salary data
            //var priceSalaryTask = Task.Run(async () =>
            //{
            //    using var scope = _serviceProvider.CreateScope();
            //    var dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();

            //    result.ExpenseList.SalaryExpense = await dbContext.Salarys
            //        .AsNoTracking()
            //        .Where(x => x.Month == month && x.Year == year && x.Employee.PharmacyId == pharmacyId)
            //        .GroupBy(x => x.Month)
            //        .Select(group => new ExpenseResponse
            //        {
            //            Price = group.Sum(x => x.BasicSalary + x.Bonus),
            //            Unit = group.Key,
            //        })
            //        .OrderBy(x => x.Unit)
            //        .ToListAsync();

            //    result.ExpenseList.TotalSalary = result.ExpenseList.SalaryExpense.Sum(x => x.Price);
            //});

            //tasks.Add(priceSalaryTask);

            // Fetch receipt data
            var priceReceiptTask = Task.Run(async () =>
            {
                using var scope = _serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();

                result.ExpenseList.ReceiptExpense = await dbContext.ReceiptDetails
                    .AsNoTracking()
                    .Where(x => x.Receipt.Date.Month == month && x.Receipt.Date.Year == year && x.Receipt.PharmacyId == pharmacyId && x.CategoryId== categoryId)
                    .GroupBy(x => x.Receipt.Date.Day)
                    .Select(group => new ExpenseResponse
                    {
                        Price = group.Sum(x => x.Price * x.Quantity),
                        Unit = group.Key,
                    })
                    .OrderBy(x => x.Unit)
                    .ToListAsync();

                result.ExpenseList.TotalReceipt = result.ExpenseList.ReceiptExpense.Sum(x => x.Price);
            });

            tasks.Add(priceReceiptTask);

            // Wait for all tasks to complete
            await Task.WhenAll(tasks);

            // Calculate total revenue, expense, and profit
            result.RevenueList.TotalRevenue = result.RevenueList.TotalOrder + result.RevenueList.TotalReturnSupplier;
            result.ExpenseList.TotalExpense = result.ExpenseList.TotalReceipt;
            result.TotalProfit = result.RevenueList.TotalRevenue - result.ExpenseList.TotalExpense;

            return result;
        } 
        public async Task<StatisticsResponse> GetProfitProductAllPharmacy(int month, int year, string categoryId)
        {
            var result = new StatisticsResponse
            {
                RevenueList = new ListRevenueResponse
                {
                    OrderRevenue = new List<RevenueResponse>(),
                    ReturnSupplierRevenue = new List<RevenueResponse>()
                },
                ExpenseList = new ListExpenseResponse
                {
                    SalaryExpense = new List<ExpenseResponse>(),
                    ReceiptExpense = new List<ExpenseResponse>()
                }
            };

            var tasks = new List<Task>();

            // Fetch order data
            var priceOrderTask = Task.Run(async () =>
            {
                using var scope = _serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();

                result.RevenueList.OrderRevenue = await dbContext.Orders
                    .AsNoTracking()
                    .Include(x => x.OrderDetails)
                    .Where(x => x.Date.Month == month && x.Date.Year == year && x.OrderDetails.Any(x => x.CategoryId == categoryId))
                    .GroupBy(x => x.Date.Day)
                    .Select(group => new RevenueResponse
                    {
                        Price = group.SelectMany(x => x.OrderDetails).Sum(od => od.Quantity * od.Price),
                        Unit = group.Key,
                    })
                    .OrderBy(x => x.Unit)
                    .ToListAsync();

                result.RevenueList.TotalOrder = result.RevenueList.OrderRevenue.Sum(x => x.Price);
            });

            tasks.Add(priceOrderTask);

            // Fetch return supplier data
            var priceReturnSupplierTask = Task.Run(async () =>
            {
                using var scope = _serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();

                result.RevenueList.ReturnSupplierRevenue = await dbContext.ReturnSuppliers
                    .AsNoTracking()
                    .Where(x => x.Date.Month == month && x.Date.Year == year && x.CategoryId == categoryId)
                    .GroupBy(x => x.Date.Day)
                    .Select(group => new RevenueResponse
                    {
                        Price = group.Sum(x => x.Price * x.Quantity),
                        Unit = group.Key,
                    })
                    .OrderBy(x => x.Unit)
                    .ToListAsync();

                result.RevenueList.TotalReturnSupplier = result.RevenueList.ReturnSupplierRevenue.Sum(x => x.Price);
            });

            tasks.Add(priceReturnSupplierTask);

            // Fetch receipt data
            var priceReceiptTask = Task.Run(async () =>
            {
                using var scope = _serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();

                result.ExpenseList.ReceiptExpense = await dbContext.ReceiptDetails
                    .AsNoTracking()
                    .Where(x => x.Receipt.Date.Month == month && x.Receipt.Date.Year == year && x.CategoryId == categoryId)
                    .GroupBy(x => x.Receipt.Date.Day)
                    .Select(group => new ExpenseResponse
                    {
                        Price = group.Sum(x => x.Price * x.Quantity),
                        Unit = group.Key,
                    })
                    .OrderBy(x => x.Unit)
                    .ToListAsync();

                result.ExpenseList.TotalReceipt = result.ExpenseList.ReceiptExpense.Sum(x => x.Price);
            });

            tasks.Add(priceReceiptTask);

            // Wait for all tasks to complete
            await Task.WhenAll(tasks);

            // Calculate total revenue, expense, and profit
            result.RevenueList.TotalRevenue = result.RevenueList.TotalOrder + result.RevenueList.TotalReturnSupplier;
            result.ExpenseList.TotalExpense = result.ExpenseList.TotalReceipt;
            result.TotalProfit = result.RevenueList.TotalRevenue - result.ExpenseList.TotalExpense;

            return result;
        }
        public async Task<StatisticsResponse> GetProfitProductWithPharmacy(int year, int pharmacyId, string categoryId)
        {
            var result = new StatisticsResponse
            {
                RevenueList = new ListRevenueResponse
                {
                    OrderRevenue = new List<RevenueResponse>(),
                    ReturnSupplierRevenue = new List<RevenueResponse>()
                },
                ExpenseList = new ListExpenseResponse
                {
                    SalaryExpense = new List<ExpenseResponse>(),
                    ReceiptExpense = new List<ExpenseResponse>()
                }
            };

            var tasks = new List<Task>();

            // Fetch order data
            var priceOrderTask = Task.Run(async () =>
            {
                using var scope = _serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();

                result.RevenueList.OrderRevenue = await dbContext.Orders
                    .AsNoTracking()
                    .Include(x => x.OrderDetails)
                    .Where(x => x.Date.Year == year && x.PharmacyId == pharmacyId && x.OrderDetails.Any(x => x.CategoryId == categoryId))
                    .GroupBy(x => x.Date.Day)
                    .Select(group => new RevenueResponse
                    {
                        Price = group.SelectMany(x => x.OrderDetails).Sum(od => od.Quantity * od.Price),
                        Unit = group.Key,
                    })
                    .OrderBy(x => x.Unit)
                    .ToListAsync();

                result.RevenueList.TotalOrder = result.RevenueList.OrderRevenue.Sum(x => x.Price);
            });

            tasks.Add(priceOrderTask);

            // Fetch return supplier data
            var priceReturnSupplierTask = Task.Run(async () =>
            {
                using var scope = _serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();

                result.RevenueList.ReturnSupplierRevenue = await dbContext.ReturnSuppliers
                    .AsNoTracking()
                    .Where(x => x.Date.Year == year && x.Medicine.PharmacyId == pharmacyId && x.CategoryId == categoryId)
                    .GroupBy(x => x.Date.Day)
                    .Select(group => new RevenueResponse
                    {
                        Price = group.Sum(x => x.Price * x.Quantity),
                        Unit = group.Key,
                    })
                    .OrderBy(x => x.Unit)
                    .ToListAsync();

                result.RevenueList.TotalReturnSupplier = result.RevenueList.ReturnSupplierRevenue.Sum(x => x.Price);
            });

            tasks.Add(priceReturnSupplierTask);

            // Fetch salary data
            //var priceSalaryTask = Task.Run(async () =>
            //{
            //    using var scope = _serviceProvider.CreateScope();
            //    var dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();

            //    result.ExpenseList.SalaryExpense = await dbContext.Salarys
            //        .AsNoTracking()
            //        .Where(x => x.Month == month && x.Year == year && x.Employee.PharmacyId == pharmacyId)
            //        .GroupBy(x => x.Month)
            //        .Select(group => new ExpenseResponse
            //        {
            //            Price = group.Sum(x => x.BasicSalary + x.Bonus),
            //            Unit = group.Key,
            //        })
            //        .OrderBy(x => x.Unit)
            //        .ToListAsync();

            //    result.ExpenseList.TotalSalary = result.ExpenseList.SalaryExpense.Sum(x => x.Price);
            //});

            //tasks.Add(priceSalaryTask);

            // Fetch receipt data
            var priceReceiptTask = Task.Run(async () =>
            {
                using var scope = _serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();

                result.ExpenseList.ReceiptExpense = await dbContext.ReceiptDetails
                    .AsNoTracking()
                    .Where(x => x.Receipt.Date.Year == year && x.Receipt.PharmacyId == pharmacyId && x.CategoryId == categoryId)
                    .GroupBy(x => x.Receipt.Date.Day)
                    .Select(group => new ExpenseResponse
                    {
                        Price = group.Sum(x => x.Price * x.Quantity),
                        Unit = group.Key,
                    })
                    .OrderBy(x => x.Unit)
                    .ToListAsync();

                result.ExpenseList.TotalReceipt = result.ExpenseList.ReceiptExpense.Sum(x => x.Price);
            });

            tasks.Add(priceReceiptTask);

            // Wait for all tasks to complete
            await Task.WhenAll(tasks);

            // Calculate total revenue, expense, and profit
            result.RevenueList.TotalRevenue = result.RevenueList.TotalOrder + result.RevenueList.TotalReturnSupplier;
            result.ExpenseList.TotalExpense = result.ExpenseList.TotalReceipt;
            result.TotalProfit = result.RevenueList.TotalRevenue - result.ExpenseList.TotalExpense;

            return result;
        }
        public async Task<StatisticsResponse> GetProfitProductAllPharmacy(int year, string categoryId)
        {
            var result = new StatisticsResponse
            {
                RevenueList = new ListRevenueResponse
                {
                    OrderRevenue = new List<RevenueResponse>(),
                    ReturnSupplierRevenue = new List<RevenueResponse>()
                },
                ExpenseList = new ListExpenseResponse
                {
                    SalaryExpense = new List<ExpenseResponse>(),
                    ReceiptExpense = new List<ExpenseResponse>()
                }
            };

            var tasks = new List<Task>();

            // Fetch order data
            var priceOrderTask = Task.Run(async () =>
            {
                using var scope = _serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();

                result.RevenueList.OrderRevenue = await dbContext.Orders
                    .AsNoTracking()
                    .Include(x => x.OrderDetails)
                    .Where(x => x.Date.Year == year && x.OrderDetails.Any(x => x.CategoryId == categoryId))
                    .GroupBy(x => x.Date.Day)
                    .Select(group => new RevenueResponse
                    {
                        Price = group.SelectMany(x => x.OrderDetails).Sum(od => od.Quantity * od.Price),
                        Unit = group.Key,
                    })
                    .OrderBy(x => x.Unit)
                    .ToListAsync();

                result.RevenueList.TotalOrder = result.RevenueList.OrderRevenue.Sum(x => x.Price);
            });

            tasks.Add(priceOrderTask);

            // Fetch return supplier data
            var priceReturnSupplierTask = Task.Run(async () =>
            {
                using var scope = _serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();

                result.RevenueList.ReturnSupplierRevenue = await dbContext.ReturnSuppliers
                    .AsNoTracking()
                    .Where(x => x.Date.Year == year && x.CategoryId == categoryId)
                    .GroupBy(x => x.Date.Day)
                    .Select(group => new RevenueResponse
                    {
                        Price = group.Sum(x => x.Price * x.Quantity),
                        Unit = group.Key,
                    })
                    .OrderBy(x => x.Unit)
                    .ToListAsync();

                result.RevenueList.TotalReturnSupplier = result.RevenueList.ReturnSupplierRevenue.Sum(x => x.Price);
            });

            tasks.Add(priceReturnSupplierTask);

            // Fetch receipt data
            var priceReceiptTask = Task.Run(async () =>
            {
                using var scope = _serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();

                result.ExpenseList.ReceiptExpense = await dbContext.ReceiptDetails
                    .AsNoTracking()
                    .Where(x =>x.Receipt.Date.Year == year && x.CategoryId == categoryId)
                    .GroupBy(x => x.Receipt.Date.Day)
                    .Select(group => new ExpenseResponse
                    {
                        Price = group.Sum(x => x.Price * x.Quantity),
                        Unit = group.Key,
                    })
                    .OrderBy(x => x.Unit)
                    .ToListAsync();

                result.ExpenseList.TotalReceipt = result.ExpenseList.ReceiptExpense.Sum(x => x.Price);
            });

            tasks.Add(priceReceiptTask);

            // Wait for all tasks to complete
            await Task.WhenAll(tasks);

            // Calculate total revenue, expense, and profit
            result.RevenueList.TotalRevenue = result.RevenueList.TotalOrder + result.RevenueList.TotalReturnSupplier;
            result.ExpenseList.TotalExpense = result.ExpenseList.TotalReceipt;
            result.TotalProfit = result.RevenueList.TotalRevenue - result.ExpenseList.TotalExpense;

            return result;
        }
        public async Task<StatisticsResponse> GetProfitProductAllPharmacy(DateOnly fromDate, DateOnly toDate,string categoryId)
        {
            var result = new StatisticsResponse
            {
                RevenueList = new ListRevenueResponse
                {
                    OrderRevenue = new List<RevenueResponse>(),
                    ReturnSupplierRevenue = new List<RevenueResponse>()
                },
                ExpenseList = new ListExpenseResponse
                {
                    SalaryExpense = new List<ExpenseResponse>(),
                    ReceiptExpense = new List<ExpenseResponse>()
                }
            };

            var tasks = new List<Task>();

            // Fetch order data
            var priceOrderTask = Task.Run(async () =>
            {
                using var scope = _serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();

                result.RevenueList.OrderRevenue = await dbContext.Orders
                    .AsNoTracking()
                    .Include(x => x.OrderDetails)
                    .Where(x => x.Date >= fromDate && x.Date <= toDate && x.OrderDetails.Any(x => x.CategoryId == categoryId))
                    .GroupBy(x => x.Date.Day)
                    .Select(group => new RevenueResponse
                    {
                        Price = group.SelectMany(x => x.OrderDetails).Sum(od => od.Quantity * od.Price),
                        Unit = group.Key,
                    })
                    .OrderBy(x => x.Unit)
                    .ToListAsync();

                result.RevenueList.TotalOrder = result.RevenueList.OrderRevenue.Sum(x => x.Price);
            });

            tasks.Add(priceOrderTask);

            // Fetch return supplier data
            var priceReturnSupplierTask = Task.Run(async () =>
            {
                using var scope = _serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();

                result.RevenueList.ReturnSupplierRevenue = await dbContext.ReturnSuppliers
                    .AsNoTracking()
                    .Where(x => x.Date >= fromDate && x.Date <= toDate && x.CategoryId==categoryId)
                    .GroupBy(x => x.Date.Day)
                    .Select(group => new RevenueResponse
                    {
                        Price = group.Sum(x => x.Price * x.Quantity),
                        Unit = group.Key,
                    })
                    .OrderBy(x => x.Unit)
                    .ToListAsync();

                result.RevenueList.TotalReturnSupplier = result.RevenueList.ReturnSupplierRevenue.Sum(x => x.Price);
            });

            tasks.Add(priceReturnSupplierTask);

            // Fetch salary data
            //var priceSalaryTask = Task.Run(async () =>
            //{
            //    using var scope = _serviceProvider.CreateScope();
            //    var dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();

            //    result.ExpenseList.SalaryExpense = await dbContext.Salarys
            //        .AsNoTracking()
            //        .Where(x =>
            //(x.Year > fromDate.Year || (x.Year == fromDate.Year && x.Month >= fromDate.Month)) &&
            //(x.Year < toDate.Year || (x.Year == toDate.Year && x.Month <= toDate.Month))
            //)
            //        .GroupBy(x => x.Month)
            //        .Select(group => new ExpenseResponse
            //        {
            //            Price = group.Sum(x => x.BasicSalary + x.Bonus),
            //            Unit = group.Key,
            //        })
            //        .OrderBy(x => x.Unit)
            //        .ToListAsync();

            //    result.ExpenseList.TotalSalary = result.ExpenseList.SalaryExpense.Sum(x => x.Price);
            //});

            //tasks.Add(priceSalaryTask);

            // Fetch receipt data
            var priceReceiptTask = Task.Run(async () =>
            {
                using var scope = _serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();

                result.ExpenseList.ReceiptExpense = await dbContext.ReceiptDetails
                    .AsNoTracking()
                    .Where(x => x.Receipt.Date >= fromDate && x.Receipt.Date <= toDate && x.CategoryId==categoryId)
                    .GroupBy(x => x.Receipt.Date.Day)
                    .Select(group => new ExpenseResponse
                    {
                        Price = group.Sum(x => x.Price * x.Quantity),
                        Unit = group.Key,
                    })
                    .OrderBy(x => x.Unit)
                    .ToListAsync();

                result.ExpenseList.TotalReceipt = result.ExpenseList.ReceiptExpense.Sum(x => x.Price);
            });

            tasks.Add(priceReceiptTask);

            // Wait for all tasks to complete
            await Task.WhenAll(tasks);

            // Calculate total revenue, expense, and profit
            result.RevenueList.TotalRevenue = result.RevenueList.TotalOrder + result.RevenueList.TotalReturnSupplier;
            result.ExpenseList.TotalExpense =  result.ExpenseList.TotalReceipt;
            result.TotalProfit = result.RevenueList.TotalRevenue - result.ExpenseList.TotalExpense;

            return result;
        }
        public async Task<StatisticsResponse> GetProfitWithProductPharmacy(DateOnly fromDate, DateOnly toDate, int pharmacyId, string categoryId)
        {
            var result = new StatisticsResponse
            {
                RevenueList = new ListRevenueResponse
                {
                    OrderRevenue = new List<RevenueResponse>(),
                    ReturnSupplierRevenue = new List<RevenueResponse>()
                },
                ExpenseList = new ListExpenseResponse
                {
                    SalaryExpense = new List<ExpenseResponse>(),
                    ReceiptExpense = new List<ExpenseResponse>()
                }
            };

            var tasks = new List<Task>();

            // Fetch order data
            var priceOrderTask = Task.Run(async () =>
            {
                using var scope = _serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();

                result.RevenueList.OrderRevenue = await dbContext.Orders
                    .AsNoTracking()
                    .Include(x => x.OrderDetails)
                    .Where(x => x.Date >= fromDate && x.Date <= toDate && x.PharmacyId == pharmacyId && x.OrderDetails.Any(x => x.CategoryId == categoryId))
                    .GroupBy(x => x.Date.Day)
                    .Select(group => new RevenueResponse
                    {
                        Price = group.SelectMany(x => x.OrderDetails).Sum(od => od.Quantity * od.Price),
                        Unit = group.Key,
                    })
                    .OrderBy(x => x.Unit)
                    .ToListAsync();

                result.RevenueList.TotalOrder = result.RevenueList.OrderRevenue.Sum(x => x.Price);
            });

            tasks.Add(priceOrderTask);

            // Fetch return supplier data
            var priceReturnSupplierTask = Task.Run(async () =>
            {
                using var scope = _serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();

                result.RevenueList.ReturnSupplierRevenue = await dbContext.ReturnSuppliers
                    .AsNoTracking()
                    .Where(x => x.Date >= fromDate && x.Date <= toDate && x.Medicine.PharmacyId == pharmacyId && x.CategoryId==categoryId)
                    .GroupBy(x => x.Date.Day)
                    .Select(group => new RevenueResponse
                    {
                        Price = group.Sum(x => x.Price * x.Quantity),
                        Unit = group.Key,
                    })
                    .OrderBy(x => x.Unit)
                    .ToListAsync();

                result.RevenueList.TotalReturnSupplier = result.RevenueList.ReturnSupplierRevenue.Sum(x => x.Price);
            });

            tasks.Add(priceReturnSupplierTask);

            // Fetch salary data
            //var priceSalaryTask = Task.Run(async () =>
            //{
            //    using var scope = _serviceProvider.CreateScope();
            //    var dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();

            //    result.ExpenseList.SalaryExpense = await dbContext.Salarys
            //        .AsNoTracking()
            //        .Where(x =>
            //(x.Year > fromDate.Year || (x.Year == fromDate.Year && x.Month >= fromDate.Month)) &&
            //(x.Year < toDate.Year || (x.Year == toDate.Year && x.Month <= toDate.Month))
            //&& x.Employee.PharmacyId == pharmacyId)
            //        .GroupBy(x => x.Month)
            //        .Select(group => new ExpenseResponse
            //        {
            //            Price = group.Sum(x => x.BasicSalary + x.Bonus),
            //            Unit = group.Key,
            //        })
            //        .OrderBy(x => x.Unit)
            //        .ToListAsync();

            //    result.ExpenseList.TotalSalary = result.ExpenseList.SalaryExpense.Sum(x => x.Price);
            //});

            //tasks.Add(priceSalaryTask);

            // Fetch receipt data
            var priceReceiptTask = Task.Run(async () =>
            {
                using var scope = _serviceProvider.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();

                result.ExpenseList.ReceiptExpense = await dbContext.ReceiptDetails
                    .AsNoTracking()
                    .Where(x => x.Receipt.Date >= fromDate && x.Receipt.Date <= toDate && x.Receipt.PharmacyId == pharmacyId && x.CategoryId==categoryId)
                    .GroupBy(x => x.Receipt.Date.Day)
                    .Select(group => new ExpenseResponse
                    {
                        Price = group.Sum(x => x.Price * x.Quantity),
                        Unit = group.Key,
                    })
                    .OrderBy(x => x.Unit)
                    .ToListAsync();

                result.ExpenseList.TotalReceipt = result.ExpenseList.ReceiptExpense.Sum(x => x.Price);
            });

            tasks.Add(priceReceiptTask);

            // Wait for all tasks to complete
            await Task.WhenAll(tasks);

            // Calculate total revenue, expense, and profit
            result.RevenueList.TotalRevenue = result.RevenueList.TotalOrder + result.RevenueList.TotalReturnSupplier;
            result.ExpenseList.TotalExpense = result.ExpenseList.TotalReceipt;
            result.TotalProfit = result.RevenueList.TotalRevenue - result.ExpenseList.TotalExpense;

            return result;
        }

    }
}

