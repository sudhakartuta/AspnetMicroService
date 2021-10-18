using Basket.API.Entites;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redishCache;

        public BasketRepository(IDistributedCache redishCache)
        {
            _redishCache = redishCache ?? throw new ArgumentNullException(nameof(redishCache));
        }
        public async Task<ShoppingCart> GetBasket(string userName)
        {
            var basket = await _redishCache.GetStringAsync(userName);
            if (String.IsNullOrEmpty(basket))
                return null;

            return JsonConvert.DeserializeObject<ShoppingCart>(basket);
        }
        public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
        {
            await _redishCache.SetStringAsync(basket.UserName, JsonConvert.SerializeObject(basket));
            return await GetBasket(basket.UserName);
        }
        public async Task DeleteBasket(string userName)
        {
            await _redishCache.RemoveAsync(userName);
        }

      

        
    }
}
