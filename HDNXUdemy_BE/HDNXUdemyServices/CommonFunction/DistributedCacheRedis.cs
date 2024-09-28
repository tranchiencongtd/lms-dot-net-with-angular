using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text;

namespace HDNXUdemyServices.CommonFunction
{
    public static class DistributedCacheRedis
    {
        public static async Task SetDataToDistributedCache(IDistributedCache _distributedCache, string keyValue, object valueData)
        {
            await _distributedCache.SetAsync(keyValue, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(valueData)), ConvertData.OptionCache());
        }

        public static async Task<T?> GetDataToDistributedCache<T>(IDistributedCache _distributedCache, string keyValue) where T : class
        {
            if (keyValue != null)
            {
                var resultRedis = await _distributedCache.GetAsync(keyValue);
                return resultRedis == null ? null : JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(resultRedis));
            }
            else
            {
                return null;
            }
        }

        public static async Task DeleteDataOfCache(IDistributedCache _distributedCache, string keyValue)
        {
            await _distributedCache.RemoveAsync(keyValue);
        }
    }
}