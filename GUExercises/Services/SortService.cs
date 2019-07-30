
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GUExercises.Models;

namespace GUExercises.Services
{
    public class SortService
    {
        private IExternalService _externalService;

        public SortService(IExternalService externalSerivce)
        {
            _externalService = externalSerivce;
        }

        public async Task<List<ProductModel>> Sort(string sortOption)
        {
            List<ProductModel> sortedProductList = null;

            if (!string.IsNullOrEmpty(sortOption))
            {
                sortOption = sortOption.ToLower();
                var productList = await _externalService.GetProductList();

                switch (sortOption)
                {
                    case "low":
                        sortedProductList = productList.OrderBy(p => p.Price).ToList();
                        break;
                    case "high":
                        sortedProductList = productList.OrderByDescending(p => p.Price).ToList();
                        break;
                    case "recommended":
                        sortedProductList = GetRecommendedProductList(productList).Result;
                        break;
                    case "ascending":
                        sortedProductList = productList.OrderBy(p => p.Name).ToList();
                        break;
                    case "descending":
                        sortedProductList = productList.OrderByDescending(p => p.Name).ToList();
                        break;
                    default:
                        break;
                }
            }
            return sortedProductList;
        }

        private async Task<List<ProductModel>> GetRecommendedProductList(List<ProductModel> productList)
        {
            List<ProductModel> sortedProductList = new List<ProductModel>();
            if ((productList != null) && (productList.Count > 0))
            {
                var shopperHistories = await _externalService.GetShopperHistory();
                List<string> sortedProductListFromHistory = GetSortedProductListFromHistory(shopperHistories);
                foreach (string ph in sortedProductListFromHistory) //add products from history
                {
                    List<ProductModel> foundProducts = productList.FindAll(p => (p.Name == ph));
                    if (foundProducts != null)
                        sortedProductList.AddRange(foundProducts);
                }
                foreach (ProductModel p in productList) //add products not found from history
                {
                    if (!sortedProductList.Exists(sp => (sp.Name == p.Name)))
                        sortedProductList.Add(p);

                }
            }
            return sortedProductList;
        }

        private List<string> GetSortedProductListFromHistory(List<ShopperHistoryModel> shopperHistoryList)
        {
            List<string> productList = new List<string>();
            if ((shopperHistoryList != null) && (shopperHistoryList.Count > 0))
            {
                Dictionary<string, double> productQuantity = new Dictionary<string, double>();
                foreach (ShopperHistoryModel sh in shopperHistoryList)
                {
                    if (sh.Products != null)
                    {
                        foreach (ProductModel p in sh.Products)
                        {
                            if (productQuantity.ContainsKey(p.Name))
                                productQuantity[p.Name] += p.Quantity;
                            else
                                productQuantity.Add(p.Name, p.Quantity);
                        }
                    }
                }

                List<KeyValuePair<string, double>> sortedProductQuantityList = productQuantity.ToList();
                sortedProductQuantityList.Sort((pair1, pair2) => pair2.Value.CompareTo(pair1.Value));

                productList = (from kvp in sortedProductQuantityList select kvp.Key).ToList();
            }
            return productList;
        }
    }
}
