
namespace Basket.API.Basket.DeleteBasket;

//public record DeleteBasketRequest(string UserName);
public record DeleteBasketResponse(bool IsSuccess);
public class DeleteBasketEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/basket/{UserName}", async (string UserName, ISender sender) =>
        {
            var result = await sender.Send(new DeleteBasketCommand(UserName));
            
            var respone = result.Adapt<DeleteBasketResponse>();

            return Results.Ok(respone);
        })
            .WithName("Delete Basket")
            .Produces<DeleteBasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete Basket Summary")
            .WithDescription("Delete Basket Description");

    }
}
