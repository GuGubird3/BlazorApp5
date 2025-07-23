using System.ComponentModel.DataAnnotations;

namespace BlazorApp5.SQLServer.Models
{
    public class LineItem
    {
        public int Id { get; set; }
        public string? Customer { get; set; }
        public string? lineName { get; set; }
        public string? partClass { get; set; }
        public string? csfileName { get; set; }
    }

    public class LineItemGroup
    {
        public IReadOnlyCollection<LineItem>? LineItems { get; set; }
    }
}
