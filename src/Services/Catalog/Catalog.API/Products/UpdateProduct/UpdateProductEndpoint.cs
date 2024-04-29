using Carter;
using Catalog.API.Products.GetProductsByCategory;

namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductResponse(bool Success);
    public record UpdateProductRequest(Guid Id, List<string> Category,string Name, string Description, string ImageFile, decimal Price);

    public class UpdateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/products", async (UpdateProductCommand UpdateProductRequest, ISender sender) =>
            {
                var command = UpdateProductRequest.Adapt<UpdateProductCommand>();
                var result = await sender.Send(command);
                var response = result.Adapt<UpdateProductResponse>();
                return Results.Ok(response);
            })
                .WithName("UpdateProduct")
                .Produces<UpdateProductResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Update Product Summary")
                .WithDescription("Update Product Description");

        }
    }
}
