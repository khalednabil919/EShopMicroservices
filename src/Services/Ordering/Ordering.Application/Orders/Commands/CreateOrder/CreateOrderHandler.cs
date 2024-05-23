﻿

namespace Ordering.Application.Orders.Commands.CreateOrder;

public class CreateOrderHandler(IApplicationDbContext dbContext) : 
    ICommandHandler<CreateOrderCommand, CreateOrderResult>
{
    public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var order = CreateNewOrder(command.Order);

        dbContext.Orders.Add(order);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateOrderResult(order.Id.Value);
    }
    private Order CreateNewOrder(OrderDto orderDto)
    {
        var items = new List<OrderItem>();
        var shippingAddress = Address.of(orderDto.ShippingAddress.FirstName, orderDto.ShippingAddress.LastName,
                                         orderDto.ShippingAddress.EmailAddress, orderDto.ShippingAddress.AddressLine,
                                         orderDto.ShippingAddress.Country, orderDto.ShippingAddress.State, orderDto.ShippingAddress.ZipCode);

        var billingAddress = Address.of(orderDto.BillingAddress.FirstName, orderDto.BillingAddress.LastName,
                                         orderDto.BillingAddress.EmailAddress, orderDto.BillingAddress.AddressLine,
                                         orderDto.BillingAddress.Country, orderDto.BillingAddress.State, orderDto.BillingAddress.ZipCode);

        var payment = Payment.of(orderDto.Payment.CardName, orderDto.Payment.CardNumber, orderDto.Payment.Expiration, orderDto.Payment.Cvv,
                                orderDto.Payment.PaymentMethod);
        var orderCreated = Order.Create(OrderId.of(orderDto.Id),
                                        CustomerId.of(orderDto.CustomerId),
                                        OrderName.of(orderDto.OrderName),
                                        shippingAddress, billingAddress, payment);

        foreach(var item in orderDto.OrderItems)
        {
            orderCreated.Add(ProductId.of(item.ProductId), item.Quantity, item.Price);
        }

        return orderCreated;
    }
}
