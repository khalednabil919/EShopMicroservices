﻿namespace Basket.API.Data
{
    public interface IBasketRepository
    {
        Task<ShoppingCart> GetBasket(string UserName, CancellationToken cancellationToken = default);
        Task<string> StoreBasket(ShoppingCart shoppingCart, CancellationToken cancellationToken = default);
        Task<bool> DeleteBasket(string UserName, CancellationToken cancellationToken = default);
    }
}
