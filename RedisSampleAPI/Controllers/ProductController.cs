using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using RedisSampleAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RedisSampleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly string url = "http://apitester.ir/api/Products";
        private readonly string cacheKey = "products_cacheKey";
        private readonly IDistributedCache distributedCache;
        public ProductController(IDistributedCache distributedCache)
        {
            this.distributedCache = distributedCache;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = new List<ProductViewModel>();
            var productsByte = await distributedCache.GetAsync(cacheKey);

            if (productsByte != null)
            {
                var productsString = Encoding.UTF8.GetString(productsByte);
                products = JsonConvert.DeserializeObject<List<ProductViewModel>>(productsString);
            }
            else
            {
                products =  await GetAsync<List<ProductViewModel>>(url);
                var serializedProducts = JsonConvert.SerializeObject(products);
                var productsBytes = Encoding.UTF8.GetBytes(serializedProducts);
                var options = new DistributedCacheEntryOptions()
                                .SetSlidingExpiration(TimeSpan.FromMinutes(5))
                                .SetAbsoluteExpiration(DateTime.Now.AddHours(6));
                await distributedCache.SetAsync(cacheKey, productsBytes, options);
            }

            return Ok(products);
        }

        private static HttpClient GetHttpClient(string url)
        {
            var client = new HttpClient { BaseAddress = new Uri(url) };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

        private static async Task<T> GetAsync<T>(string url, string urlParameters = null)
        {
            try
            {
                using (var client = GetHttpClient(url))
                {
                    HttpResponseMessage response = await client.GetAsync(urlParameters);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<T>(json);
                        return result;
                    }

                    return default;
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
