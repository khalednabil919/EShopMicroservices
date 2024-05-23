
using Ordering.Application.Orders.Queries.GetOrdersByCustomer;

namespace Ordering.API.EndPoints;

public record GetOrdersByCustomerResponse(IEnumerable<OrderDto>Orders);
public class GetOrdersByCustomer : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders/customer/{Id}", async (Guid Id, ISender sender) =>
        {
            var result = await sender.Send(new GetOrdersByCustomerQuery(Id));
            var response = result.Adapt<GetOrdersByCustomerResponse>();
            return Results.Ok(response);
        })
        .WithName("GetOrderByCustomer")
        .Produces<GetOrdersByCustomerResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("GetOrderByCustomer")
        .WithDescription("GetOrderByCustomer");

    }
}
