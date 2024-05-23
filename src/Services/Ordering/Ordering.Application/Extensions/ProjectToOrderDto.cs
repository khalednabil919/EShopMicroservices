namespace Ordering.Application.Extensions;
public static class ProjectToOrderDto
{
    public static List<OrderDto>ToOrderDtoList(this List<Order> orders)
    {
        List<OrderDto> orderDtos = new List<OrderDto>();
        foreach (var item in orders)
        {
            var shippingAddess = new AddressDto(item.ShippingAddress.FirstName, item.ShippingAddress.LastName, item.ShippingAddress.EmailName!,
                                                item.ShippingAddress.AddressLine, item.ShippingAddress.Country, item.ShippingAddress.State,
                                                item.ShippingAddress.ZipCode);

            var billingAddess = new AddressDto(item.BillingAddress.FirstName, item.BillingAddress.LastName, item.BillingAddress.EmailName!,
                                    item.BillingAddress.AddressLine, item.BillingAddress.Country, item.BillingAddress.State,
                                    item.BillingAddress.ZipCode);

            var payment = new PaymentDto(item.Payment.CardName!, item.Payment.CardNumber, item.Payment.Expiration, item.Payment.CVV,
                                        item.Payment.PaymentMethod);

            List<OrderItemDto> orderItems = item.OrderItems.Select
                                                (x => new OrderItemDto(x.OrderId.Value, x.ProductId.Value, x.Quantity, x.Price)).ToList();
            orderDtos.Add(new OrderDto(Id: item.Id.Value, CustomerId: item.CustomerId.Value, OrderName: item.OrderName.Value,
                                       ShippingAddress: shippingAddess, BillingAddress: billingAddess, Payment: payment, Status: item.Status,
                                       OrderItems: orderItems));
        }
        return orderDtos;
    }
}
