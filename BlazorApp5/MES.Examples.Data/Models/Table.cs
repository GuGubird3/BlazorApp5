using MudBlazor.Examples.Data.Models;
using System.Text.Json.Serialization;

namespace BlazorApp5.MES.Examples.Data.Models
{
    public class Table
    {
        public IReadOnlyCollection<LineItemGroup>? LineItemGroups { get; set; }
    }
}
