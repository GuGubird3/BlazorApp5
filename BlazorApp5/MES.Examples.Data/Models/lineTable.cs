using System.Text.Json.Serialization;

namespace BlazorApp5.MES.Examples.Data.Models
{
    public class lineTable
    {
        public IReadOnlyCollection<LineItemGroup>? LineItemGroups { get; set; }
    }
}
