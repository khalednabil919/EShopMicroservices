﻿using Carter;

namespace Catalog.API.Products.GetProducts
{
    public record GetProductResponse(IEnumerable<Product>Products);
    public class GetProductsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products", async (ISender sender) =>
            {
                var result = await sender.Send(new GetProductQuery());
                var response = result.Adapt<GetProductResponse>();
                return Results.Ok(response);
            })
                .WithName("GetProducts")
                .Produces<GetProductResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Product Summary")
                .WithDescription("Get Product Description");

        }
    }
}
