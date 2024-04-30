using Carter;
using Catalog.API.Products.GetProductById;

namespace Catalog.API.Products.DeleteProduct
{
    public class DeleteProductResponse
    {
        public Guid IsSuccess { get; set; }
    }
    public class DeleteProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/products/{Id}", async (Guid Id, ISender sender) =>
            {
                var product = await sender.Send(new DeleteProductCommand { Id = Id });
                var response = product.Adapt<DeleteProductResponse>();
                return Results.Ok(response);
            })
                .WithName("DeleteProduct")
                .Produces<DeleteProductResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("DeleteProduct Summary")
                .WithDescription("DeleteProduct Description");

        }
    }
}
