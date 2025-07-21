using BlazorApp5.MES.Examples.Data.Models;

namespace BlazorApp5.MES.Examples.Data
{
    public interface ImesLineService
    {
        Task<IEnumerable<LineItem>> GetLineItems();
    }
}
