
using Marten.Linq.SoftDeletes;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.API.Data
{
    public class CachedBasketRepository(IBasketRepository basketRepository, IDistributedCache Cache) : IBasketRepository
    {
        public async Task<bool> DeleteBasket(string UserName, CancellationToken cancellationToken = default)
        {
            if (!string.IsNullOrWhiteSpace((await Cache.GetStringAsync(UserName, cancellationToken))))
                await Cache.RemoveAsync(UserName);

            return await basketRepository.DeleteBasket(UserName, cancellationToken);
        }

        public async Task<ShoppingCart> GetBasket(string UserName, CancellationToken cancellationToken = default)
        {
            var cachedBasket = await Cache.GetStringAsync(UserName, cancellationToken);
            if (!string.IsNullOrWhiteSpace(cachedBasket))
                return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket!)!;

            var basket = await basketRepository.GetBasket(UserName, cancellationToken);
            await Cache.SetStringAsync(UserName, JsonSerializer.Serialize(basket));
            return basket;
        }

        public async Task<ShoppingCart> StoreBasket(ShoppingCart shoppingCart, CancellationToken cancellationToken = default)
        {
            await basketRepository.StoreBasket(shoppingCart, cancellationToken);
            await Cache.SetStringAsync(shoppingCart.UserName, JsonSerializer.Serialize(shoppingCart));
            return shoppingCart;
        }
    }
}
