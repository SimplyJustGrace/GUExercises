using GUExercises.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GUExercises.Services
{
    public interface IExternalService
    {
        Task<List<ProductModel>> GetProductList();
        Task<List<ShopperHistoryModel>> GetShopperHistory();
    }
}
