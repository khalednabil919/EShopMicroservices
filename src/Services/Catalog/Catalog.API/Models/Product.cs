using System.ComponentModel;

namespace Catalog.API.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public List<string> Category = new();
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string ImageFile { get; set; } = default!;
        public decimal Price { get; set; }
    }
}
