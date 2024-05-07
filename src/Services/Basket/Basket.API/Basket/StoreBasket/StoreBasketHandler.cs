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
public class StoreBasketCommandHandler(IBasketRepository _basketRepository) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand request, CancellationToken cancellationToken)
    {
        //TODO: update cache
        var kh = _basketRepository;
        await _basketRepository.StoreBasket(request.Cart, cancellationToken);
        return new StoreBasketResult(request.Cart.UserName);
    }
}
