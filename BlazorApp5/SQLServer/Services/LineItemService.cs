using BlazorApp5.SQLServer.Data;
using BlazorApp5.SQLServer.Models;

namespace BlazorApp5.SQLServer.Services
{
    public class LineItemService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        //private readonly HttpClient _httpClient;

        public LineItemService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            //_httpClient = _httpClientFactory.CreateClient("ServerAPI");
        }

        private HttpClient GetClient() => _httpClientFactory.CreateClient("ServerAPI");


        public async Task<List<LineItem>> GetAllAsync()
        {
            var client = GetClient();
            return await client.GetFromJsonAsync<List<LineItem>>("api/lineitems") ?? new();
        }

        public async Task<LineItem?> GetByIdAsync(int id)
        {
            var client = GetClient();
            return await client.GetFromJsonAsync<LineItem>($"api/lineitems/{id}");
        }

        public async Task AddAsync(LineItem item)
        {
            var client = GetClient();
            await client.PostAsJsonAsync("api/lineitems", item);
        }

        public async Task UpdateAsync(LineItem item)
        {
            var client = GetClient();
            await client.PutAsJsonAsync($"api/lineitems/{item.Id}", item);
        }

        public async Task DeleteAsync(int id)
        {
            var client = GetClient();
            await client.DeleteAsync($"api/lineitems/{id}");
        }
    }
}
