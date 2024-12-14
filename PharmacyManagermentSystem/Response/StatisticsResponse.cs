namespace PharmacyManagermentSystem.Response
{
    public class StatisticsResponse
    {
        public ListRevenueResponse RevenueList { get; set; }
        public ListExpenseResponse ExpenseList { get; set; }
        public double TotalProfit { get; set; }
        

    }
    public class ListRevenueResponse
    {
        public IList<RevenueResponse> OrderRevenue { get; set; }
        public IList<RevenueResponse> ReturnSupplierRevenue { get; set; }
        public double TotalOrder { get; set;}
        public double TotalReturnSupplier { get; set; }
        public double TotalRevenue { get; set; }
    }
    public class ListExpenseResponse
    {
        public IList<ExpenseResponse> SalaryExpense { get; set; }
        public IList<ExpenseResponse> ReceiptExpense { get; set; }
        public double TotalSalary { get; set; }
        public double TotalReceipt { get; set; }
        public double TotalExpense { get; set; }
    }
    public class RevenueResponse
    {
        public double Price { get; set; }
        public int Unit { get; set; }
    }
    public class ExpenseResponse
    {
        public double Price { get; set; }
        public int Unit { get; set; }
    }

}
