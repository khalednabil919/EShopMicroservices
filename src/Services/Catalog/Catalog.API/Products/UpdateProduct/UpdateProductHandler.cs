using BuildingBlocks.CQRS;
using Mapster;
using System.Windows.Input;

namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid Id,string Name, List<string> Category, string Description, string ImageFile, decimal Price)
        : ICommand<UpdateProductResult>;
    public record UpdateProductResult(bool Success);
    
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty()
                .WithMessage("Product Id is Required");

            RuleFor(command => command.Name).NotEmpty()
                .WithMessage("Name is Required")
                .Length(2, 150).WithMessage("Name Length should be between 2 and 150");

            RuleFor(command => command.Price).GreaterThan(0)
                .WithMessage("Price Should be greater than 0");
        }
    }

    internal class UpdateProductcommandHandler(IDocumentSession session) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var result = await session.LoadAsync<Product>(command.Id, cancellationToken);
            if (result is null)
                throw new ProductNotFoundException(command.Id);

            var req = command.Adapt<UpdateProductCommand,Product>();
            session.Update(req);
            await session.SaveChangesAsync();
            return new UpdateProductResult(true);
        }
    }
}
