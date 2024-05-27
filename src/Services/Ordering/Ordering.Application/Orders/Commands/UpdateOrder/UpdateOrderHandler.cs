
namespace Ordering.Application.Orders.Commands.UpdateOrder;
public class UpdateOrderHandler(IApplicationDbContext dbContext) : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
{
    public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
    {
        var order = dbContext.Orders.Find(OrderId.of(command.Order.Id));
        if (order is null)
            throw new OrderNotFoundException(command.Order.Id);

        UpdateOrderWithNewValues(order, command.Order);

        dbContext.Orders.Update(order);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateOrderResult(true);
    }
    private void UpdateOrderWithNewValues(Order order, OrderDto orderDto)
    {
        var shippingAddress = Address.of(orderDto.ShippingAddress.FirstName, orderDto.ShippingAddress.LastName,
                                 orderDto.ShippingAddress.EmailAddress, orderDto.ShippingAddress.AddressLine,
                                 orderDto.ShippingAddress.Country, orderDto.ShippingAddress.State, orderDto.ShippingAddress.ZipCode);

        var billingAddress = Address.of(orderDto.BillingAddress.FirstName, orderDto.BillingAddress.LastName,
                                         orderDto.BillingAddress.EmailAddress, orderDto.BillingAddress.AddressLine,
                                         orderDto.BillingAddress.Country, orderDto.BillingAddress.State, orderDto.BillingAddress.ZipCode);

        var payment = Payment.of(orderDto.Payment.CardName, orderDto.Payment.CardNumber, orderDto.Payment.Expiration, orderDto.Payment.Cvv,
                                orderDto.Payment.PaymentMethod);

        order.Update(orderName: OrderName.of(orderDto.OrderName), shippingAddress: shippingAddress,
                     billingAddress: billingAddress, payment: payment, status: orderDto.Status);
    } 
}
