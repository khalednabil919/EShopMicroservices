using Carter;

namespace Catalog.API.Products.GetProductById
{

    public record GetProductByIdResponse(Product Product);
    public class GetProductByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/{Id}", async (Guid Id, ISender sender) =>
            {
                var product = await sender.Send(new GetProductByIdQuery(Id));
                var response = product.Adapt<GetProductByIdResponse>();
                return Results.Ok(response);
            })
                .WithName("GetProductById")
                .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("GetProductById Summary")
                .WithDescription("GetProductById Description");
        }
    }
}
