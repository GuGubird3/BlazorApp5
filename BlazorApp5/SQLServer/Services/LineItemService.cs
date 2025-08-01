using BlazorApp5.SQLServer.Data;
using BlazorApp5.SQLServer.Models;

namespace BlazorApp5.SQLServer.Services
{
    public class LineItemService: ILineItemService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _httpClient;

        public LineItemService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _httpClient = _httpClientFactory.CreateClient("ServerAPI");
        }

        public async Task<List<LineItem>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<LineItem>>("api/lineitems") ?? new();
        }

        public async Task<LineItem?> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<LineItem>($"api/lineitems/{id}");
        }

        public async Task AddAsync(LineItem item)
        {
            await _httpClient.PostAsJsonAsync("api/lineitems", item);
        }

        public async Task UpdateAsync(LineItem item)
        {
            await _httpClient.PutAsJsonAsync($"api/lineitems/{item.Id}", item);
        }

        public async Task DeleteAsync(int id)
        {
            await _httpClient.DeleteAsync($"api/lineitems/{id}");
        }
    }
}
