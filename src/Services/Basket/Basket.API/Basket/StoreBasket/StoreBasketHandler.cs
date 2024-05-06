namespace Basket.API.Basket.StoreBasket;
public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
public record StoreBasketResult(string UserName);
public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidator() 
    {
        RuleFor(rule => rule.Cart).NotNull().WithMessage("Cart can not null");
        RuleFor(rule => rule.Cart.UserName).NotEmpty().WithMessage("UserName is required");
    }
}
public class StoreBasketCommandHandler : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand request, CancellationToken cancellationToken)
    {
        ShoppingCart cart = request.Cart;

        //TODO: store basket in database  (use Marten upsert - if exist = update, if no  insert)
        //TODO: update cache
        return new StoreBasketResult("swn");
    }
}
