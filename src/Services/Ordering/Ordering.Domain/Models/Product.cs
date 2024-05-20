

namespace Ordering.Domain.Models
{
    public class Product:Entity<ProductId>
    {
        public string Name { get; private set; } = default!;
        public double Price { get; private set; }=default!;

        public static Product Create(ProductId Id,string name, double price)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

            return new Product { Id = Id, Name = name, Price = price };
        }
    }
}
