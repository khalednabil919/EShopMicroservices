namespace Basket.API.Basket.GetBasket;

public record GetBasketQuery(string username) : IQuery<GetBasketResult>;
public record GetBasketResult(ShoppingCart ShoppingCart);
public class GetBasketQueryHandler(IBasketRepository _basketRepository) : IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
    {
        var basket = await _basketRepository.GetBasket(query.username);

        return new GetBasketResult(basket);
    }
}
