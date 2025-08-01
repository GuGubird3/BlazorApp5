using BlazorApp5.SQLServer.Models;

namespace BlazorApp5.SQLServer.Services
{
    public interface ILineItemService
    {
        Task<List<LineItem>> GetAllAsync();
        Task<LineItem?> GetByIdAsync(int id);
        Task AddAsync(LineItem item);
        Task UpdateAsync(LineItem item);
        Task DeleteAsync(int id);
    }
}
