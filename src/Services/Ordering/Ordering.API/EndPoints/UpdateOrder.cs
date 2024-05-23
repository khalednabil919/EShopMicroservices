
using Microsoft.AspNetCore.Http.HttpResults;
using Ordering.Application.Orders.Commands.UpdateOrder;

namespace Ordering.API.EndPoints;

public record UpdateOrderRequest(OrderDto OrderDto);
public record UpdateOrderResponse(bool IsSuccess);
public class UpdateOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/Orders",async (UpdateOrderRequest request, ISender sender) =>
        {
            var order = request.Adapt<UpdateOrderCommand>();
            var returnedOrder = await sender.Send(order);
            var response = returnedOrder.Adapt<UpdateOrderResponse>();
            return Results.Ok(response);
        })
        .WithName("UpdateOrder")
        .Produces<UpdateOrderResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Update Order")
        .WithDescription("Update Order");

    }
}
