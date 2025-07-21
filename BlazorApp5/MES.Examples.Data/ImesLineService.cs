using BlazorApp5.MES.Examples.Data.Models;

namespace BlazorApp5.MES.Examples.Data
{
    public class ImesLineService
    {
        Task<IEnumerable<LineItem>> GetLineItems(string apiUrl);
    }
}
