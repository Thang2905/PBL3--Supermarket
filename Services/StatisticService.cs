using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Admin.Services
{
    public class RevenueStatistic
    {
        public string Date { get; set; }
        public double Revenue { get; set; }
    }

    public class StatisticService
    {
        private readonly AuthorizedHttpClient _httpClient;

        public StatisticService(AuthorizedHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<RevenueStatistic>> GetMonthlyRevenue(int month, int year)
        {
            var response = await _httpClient.GetFromJsonAsync<ApiResult<List<RevenueStatistic>>>(
                $"supermarket/statistic/revenue?month={month}&year={year}");
            return response?.Result ?? new List<RevenueStatistic>();
        }
    }

    public class ApiResult<T>
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public T Result { get; set; }
    }
}