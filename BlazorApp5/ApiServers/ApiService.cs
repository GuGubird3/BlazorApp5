using System.Net.Http;
using System.Threading.Tasks;
using MudBlazor.Examples.Data.Models;
using MudBlazor.Examples.Data;


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

        public async Task<List<Element>> GetTestElementAsync(string httpclientName, string routName)
        {
            var client = _httpClientFactory.CreateClient($"{httpclientName}");
            try
            {
                var fullUrl = $"{client.BaseAddress}{routName}";
                Console.WriteLine($"Request URL: {fullUrl}");
                var response = await client.GetFromJsonAsync<List<Element>>($"{routName}");
                if (response == null)
                {
                    throw new Exception("Response was null");
                }
                else
                {
                    return response;
                }
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
