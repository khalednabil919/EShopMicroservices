using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Orders.Queries.GetOrdersByCustomer;

public record GetOrdersByCustomerQuery(Guid customerId) :IQuery<GetOrderByCustomerResult>;
public record GetOrderByCustomerResult(List<OrderDto> Orders);
