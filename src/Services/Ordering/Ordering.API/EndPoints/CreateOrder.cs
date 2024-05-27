using Ordering.Application.Orders.Commands.CreateOrder;
namespace Ordering.API.EndPoints;
public record CreateOrderRequest(OrderDto Order);
public record CreateOrderResponse(Guid Id);
public class CreateOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
            {
        app.MapPost("/Orders", async (CreateOrderRequest request, ISender sender) =>
        {
            var mappedRequest = request.Adapt<CreateOrderCommand>();
                var response = await sender.Send(mappedRequest);
            var orderCreated = response.Adapt<CreateOrderResponse>();           
            return Results.Created($"Orders/{orderCreated.Id}", orderCreated);
        })
        .WithName("CreateOrder")
        .Produces<CreateOrderResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Order")
        .WithDescription("Create Order");
    }
}
