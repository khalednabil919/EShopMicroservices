using BuildingBlocks.Pagination;
using Ordering.Application.Orders.Queries.GetOrders;

namespace Ordering.API.EndPoints;
public record GetOrdersRequest(PaginationRequest PaginationRequest);
public record GetOrdersResponse(PaginatedResult<OrderDto> Orders);
public class GetOrdersByPagination : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders/GetPagination/", async ([AsParameters] PaginationRequest Request, ISender sender) =>
        {
            var response = await sender.Send(new GetOrdersQuery(Request));
            var returned = response.Adapt<GetOrdersResponse>();
            return Results.Ok(returned);
        })
        .WithName("GetPaginatedOrder")
        .Produces<GetOrdersResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get PaginatedOrder")
        .WithDescription("Get PaginatedOrder");
    }
}
