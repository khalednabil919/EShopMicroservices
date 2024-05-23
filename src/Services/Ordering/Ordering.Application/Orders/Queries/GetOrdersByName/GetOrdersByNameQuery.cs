namespace Ordering.Application.Orders.Queries.GetOrdersByName;

public record GetOrdersByNameQuery(string name):IQuery<GetOrdersByNameResult>;
public record GetOrdersByNameResult(IEnumerable<OrderDto>Orders);