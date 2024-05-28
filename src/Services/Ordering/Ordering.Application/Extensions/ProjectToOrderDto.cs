using Ordering.Application.Dtos;

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
    public static OrderDto ToOrderDTO(this Order order)
    {
        var shippingAddess = new AddressDto(order.ShippingAddress.FirstName, order.ShippingAddress.LastName, order.ShippingAddress.EmailName!,
                                                order.ShippingAddress.AddressLine, order.ShippingAddress.Country, order.ShippingAddress.State,
                                                order.ShippingAddress.ZipCode);

        var billingAddess = new AddressDto(order.BillingAddress.FirstName, order.BillingAddress.LastName, order.BillingAddress.EmailName!,
                                order.BillingAddress.AddressLine, order.BillingAddress.Country, order.BillingAddress.State,
                                order.BillingAddress.ZipCode);

        var payment = new PaymentDto(order.Payment.CardName!, order.Payment.CardNumber, order.Payment.Expiration, order.Payment.CVV,
                                    order.Payment.PaymentMethod);

        List<OrderItemDto> orderItems = order.OrderItems.Select
                                            (x => new OrderItemDto(x.OrderId.Value, x.ProductId.Value, x.Quantity, x.Price)).ToList();
        return new OrderDto(Id: order.Id.Value, CustomerId: order.CustomerId.Value, OrderName: order.OrderName.Value,
                                   ShippingAddress: shippingAddess, BillingAddress: billingAddess, Payment: payment, Status: order.Status,
                                   OrderItems: orderItems);
    }
}
