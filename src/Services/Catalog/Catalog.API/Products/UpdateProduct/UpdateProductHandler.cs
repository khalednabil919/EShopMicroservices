using BuildingBlocks.CQRS;
using Mapster;
using System.Windows.Input;

namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid Id,string Name, List<string> Category, string Description, string ImageFile, decimal Price)
        : ICommand<UpdateProductResult>;
    public record UpdateProductResult(bool Success);
    internal class UpdateProductcommandHandler(IDocumentSession session) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var result = await session.LoadAsync<Product>(command.Id, cancellationToken);
            if (result is null)
                throw new ProductNotFoundException();

            var req = command.Adapt<UpdateProductCommand,Product>();
            session.Update(req);
            await session.SaveChangesAsync();
            return new UpdateProductResult(true);
        }
    }
}
