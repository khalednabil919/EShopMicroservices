using Discount.Grpc;
using MediatR;

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
public class StoreBasketCommandHandler(IBasketRepository _basketRepository, DiscountProtoService.DiscountProtoServiceClient discountGrpc) :
                                ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand request, CancellationToken cancellationToken)
    {
        // TODO : communicate with Discount.Grpc and calculate latest prices of product 
        await DeductDiscount(request.Cart);
        //TODO: update cache
        await _basketRepository.StoreBasket(request.Cart, cancellationToken);
        return new StoreBasketResult(request.Cart.UserName); 
    }
    private async Task DeductDiscount (ShoppingCart Cart)
    {
        foreach (var item in Cart.Items)
        {
            var coupon = await discountGrpc.GetDiscountAsync(new GetDiscountRequest { ProductName = item.ProductName });
            item.Price -= coupon is null?0:coupon.Amount;
        }
    }
}
