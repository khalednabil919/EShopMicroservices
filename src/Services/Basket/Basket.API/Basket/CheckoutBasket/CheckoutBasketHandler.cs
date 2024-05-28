using Basket.API.Dtos;
using BuildingBlocks.Messaging.Events;
using MassTransit;
namespace Basket.API.Basket.CheckoutBasket;
public record CheckoutBasketCommand(BasketCheckoutDto BasketCheckoutDto):ICommand<CheckoutBasketResult>;
public record CheckoutBasketResult(bool IsSuccess);
public class CheckoutBasketCommandHandler(IBasketRepository basketRepository, IPublishEndpoint publishEndpoint)
    : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
{
    public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken)
    {
        var basket = await basketRepository.GetBasket(command.BasketCheckoutDto.UserName, cancellationToken);

        if (basket == null)
            return new CheckoutBasketResult(true);

        var eventMessage = command.Adapt<BasketCheckOutEvent>();
        eventMessage.TotalPrice = (double)basket.TotalPrice;

        await publishEndpoint.Publish(eventMessage, cancellationToken);
        await basketRepository.DeleteBasket(basket.UserName,cancellationToken); 

        return new CheckoutBasketResult(true);
    }
}
