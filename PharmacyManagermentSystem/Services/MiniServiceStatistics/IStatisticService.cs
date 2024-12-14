using PharmacyManagermentSystem.Response;

namespace PharmacyManagermentSystem.Services.MiniServiceStatistics
{
    public interface IStatisticService
    {
        Task<StatisticsResponse> GetProfitAllPharmacy(int year);
        Task<StatisticsResponse> GetProfitAllPharmacy(int month, int year);
        Task<StatisticsResponse> GetProfitWithPharmacy(int month, int year, int pharmacyId);
        Task<StatisticsResponse> GetProfitWithPharmacy(int year, int pharmacyId);
        Task<StatisticsResponse> GetProfitAllPharmacy(DateOnly fromDate, DateOnly toDate);
        Task<StatisticsResponse> GetProfitWithPharmacy(DateOnly fromDate, DateOnly toDate, int pharmacyId);
        Task<StatisticsResponse> GetProfitProductWithPharmacy(int month, int year, int pharmacyId, string categoryId);
        Task<StatisticsResponse> GetProfitProductAllPharmacy(int month, int year, string categoryId);
        Task<StatisticsResponse> GetProfitProductWithPharmacy(int year, int pharmacyId, string categoryId);
        Task<StatisticsResponse> GetProfitProductAllPharmacy(int year, string categoryId);
        Task<StatisticsResponse> GetProfitProductAllPharmacy(DateOnly fromDate, DateOnly toDate, string categoryId);
        Task<StatisticsResponse> GetProfitWithProductPharmacy(DateOnly fromDate, DateOnly toDate, int pharmacyId, string categoryId);
        Task<List<GetTopSellByAllPharmacyResponse>> GetTopSellByAllPharmacy(int month, int year);
        Task<List<GetTopSellByAllPharmacyResponse>> GetTopSellByPharmacy(int month, int year, int pharmacyId);
    }
}
