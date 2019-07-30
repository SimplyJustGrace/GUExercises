using GUExercises.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace GUExercises.Services
{
    public class ExternalService : IExternalService
    {
        private ExternalSettings _externalSettings;

        public ExternalService(IOptions<ExternalSettings> externalSettingsOptions)
        {
            _externalSettings = externalSettingsOptions.Value;
        }

        public async Task<List<ProductModel>> GetProductList()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync($"{_externalSettings.ExternalAPIUrl}/api/resource/products?token={_externalSettings.ExternalAPIToken}");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var productList = JsonConvert.DeserializeObject<List<ProductModel>>(responseBody);
            return productList;
        }

        public async Task<List<ShopperHistoryModel>> GetShopperHistory()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync($"{_externalSettings.ExternalAPIUrl}/api/resource/shopperHistory?token={_externalSettings.ExternalAPIToken}");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var shopperHistoryModels = JsonConvert.DeserializeObject<List<ShopperHistoryModel>>(responseBody);
            return shopperHistoryModels;
        }
    }
}
