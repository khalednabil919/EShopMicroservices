
using Ordering.Application.Orders.Queries.GetOrdersByName;

namespace Ordering.API.EndPoints;
public record GetOrderByNameResponse(IEnumerable<OrderDto>Orders);
public class GetOrderByName : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("orders/{orderName}", async (string orderName, ISender sender) =>
        {
            var response = await sender.Send(new GetOrdersByNameQuery(orderName));
            var returnedResponse = response.Adapt<GetOrderByNameResponse>();
            return Results.Ok(returnedResponse);
        })
        .WithName("GetOrderByName")
        .Produces<GetOrderByNameResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("GetOrderByName")
        .WithDescription("GetOrderByName");

    }
}
