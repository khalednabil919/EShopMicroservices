using BuildingBlocks.CQRS;

namespace Catalog.API.Products.GetProductsByCategory
{
    public record GetProductsByCategoryQuery(string Name):IQuery<GetProductsByCategoryResult>;
    public record GetProductsByCategoryResult(IEnumerable<Product>Products);
    internal class GetProductsByCategoryQueryHandler(IDocumentSession session) : IQueryHandler<GetProductsByCategoryQuery, GetProductsByCategoryResult>
    {
        public async Task<GetProductsByCategoryResult> Handle(GetProductsByCategoryQuery query, CancellationToken cancellationToken)
        {
            var response = session.Query<Product>().Where(x => x.Category.Any(c => c == query.Name)).ToList();
            return new GetProductsByCategoryResult(response);
        }

    }
}
