using System.Net.Http;
using System.Threading.Tasks;


namespace BlazorApp5.ApiServers
{
    public class ApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        public async Task<string> GetTestStringAsync(string httpclientName,string routName)
        {
            var client = _httpClientFactory.CreateClient($"{httpclientName}");
            try
            {
                var response = await client.GetAsync($"{routName}");
                response.EnsureSuccessStatusCode(); // 确保响应成功
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                // 处理异常
                Console.WriteLine($"Error fetching test string: {ex.Message}");
                throw;
            }
        }
    }
}
