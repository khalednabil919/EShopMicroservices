using Carter;
using Catalog.API.Products.GetProducts;

namespace Catalog.API.Products.GetProductsByCategory
{
    public record GetProductByCategoryResponse(IEnumerable<Product> Products);
    public record GetProductByCategoryRequest(string Name);
    public class GetProductsByCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/products/GetProductByCategoryName", async (GetProductByCategoryRequest GetProductByCategoryRequest, ISender sender) =>
            {
                var request = GetProductByCategoryRequest.Adapt<GetProductsByCategoryQuery>();
                var products = await sender.Send(request);
                return Results.Ok(products.Adapt<GetProductByCategoryResponse>());
            })
                .WithName("GetProductByCategoryName")
                .Produces<GetProductByCategoryResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Product Summary")
                .WithDescription("Get Product Description");
        }
    }
}
