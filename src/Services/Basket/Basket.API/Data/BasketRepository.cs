﻿namespace Basket.API.Data;
public class BasketRepository(IDocumentSession session) : IBasketRepository
{
    public async Task<ShoppingCart> GetBasket(string UserName, CancellationToken cancellationToken = default)
    {
        var basket = await session.LoadAsync<ShoppingCart>(UserName,cancellationToken);
        return basket is null? throw new BasketNotFoundException(UserName) : basket;
    } 

    public async Task<string> StoreBasket(ShoppingCart shoppingCart, CancellationToken cancellationToken = default)
    {
        session.Store(shoppingCart);
        await session.SaveChangesAsync(cancellationToken);
        return shoppingCart.UserName;
    }
    public async Task<bool> DeleteBasket(string UserName, CancellationToken cancellationToken = default)
    {
        session.Delete<ShoppingCart>(UserName);
        await session.SaveChangesAsync(cancellationToken);
        return true;
    }
}
