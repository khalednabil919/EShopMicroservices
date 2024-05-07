
using Mapster;

namespace Basket.API.Basket.StoreBasket;

public record StoreBasketRequest(ShoppingCart Cart);
public record StoreBasketResponse(string UserName);
public class StoreBasketEndpoints : ICarterModule 
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/basket", async (StoreBasketRequest request, ISender sender) =>
        {
            var command = request.Adapt<StoreBasketCommand>();
            var response = await sender.Send(command);
            var result = response.Adapt<StoreBasketResponse>();
            return Results.Created($"/basket/{result.UserName}", result);
        })
            .WithName("Store basket")
            .Produces<StoreBasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Store basket Summary")
            .WithDescription("Store basket Description");
    }
}
