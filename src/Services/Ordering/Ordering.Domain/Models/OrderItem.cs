using Ordering.Domain.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace Ordering.Domain.Models;
public class OrderItem:Entity<OrderItemId>
{
    public OrderItem(OrderId orderId, ProductId productId, int quantity, double price)
    {
        Id = OrderItemId.of(Guid.NewGuid());
        OrderId = orderId;
        ProductId = productId;
        Quantity = quantity;
        Price = price; 
    }
    public OrderId OrderId { get; private set; } = default!;
    public ProductId ProductId { get; private set; } = default!;
    public int Quantity { get; private set; } = default!;
    public double Price { get; private set;} = default!;
}
