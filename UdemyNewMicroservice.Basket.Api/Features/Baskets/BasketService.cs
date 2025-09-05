using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using UdemyNewMicroservice.Basket.Api.Const;
using UdemyNewMicroservice.Shared.Services;

namespace UdemyNewMicroservice.Basket.Api.Features.Baskets
{
    public class BasketService(IIdentityService identityService, IDistributedCache distributedCache)
    {
        private string GetCacheKey() => string.Format(BasketConst.BasketCacheKey, identityService.UserId);

        private string GetCacheKey(Guid userId) => string.Format(BasketConst.BasketCacheKey, userId);

        public Task<string?> GetBasketFromCache(CancellationToken cancellationToken)
        {
            return distributedCache.GetStringAsync(GetCacheKey(), token: cancellationToken);
        }

        public async Task CreateBasketCacheAsync(Data.Basket basket, CancellationToken cancellationToken)
        {
            var basketAsString = JsonSerializer.Serialize(basket);
            await distributedCache.SetStringAsync(GetCacheKey(), basketAsString, token: cancellationToken);
        }

        public async Task DeleteBasket(Guid userId)
        {
            await distributedCache.RemoveAsync(GetCacheKey(userId));
        }
    }
}