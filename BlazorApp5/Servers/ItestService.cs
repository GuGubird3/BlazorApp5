using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorApp5.Servers
{
    public interface ItestService
    {
        public Task<string> GetTestStringAsync();

    }
}
