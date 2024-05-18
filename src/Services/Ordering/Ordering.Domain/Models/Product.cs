﻿

namespace Ordering.Domain.Models
{
    public class Product:Entity<ProductId>
    {
        public string Name { get; private set; } = default!;
        public decimal Price { get; private set; }=default!;

        public static Product Create(string name, decimal price)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

            return new Product { Id = ProductId.of(Guid.NewGuid()), Name = name, Price = price };
        }
    }
}
