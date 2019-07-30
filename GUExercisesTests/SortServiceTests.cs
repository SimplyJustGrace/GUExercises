using GUExercises.Models;
using GUExercises.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GUExercisesTests
{
    [TestClass]
    public class SortServiceTests
    {
        private SortService _sortService;

        [TestInitialize]
        public void Initialize()
        {
            var mockExternalService = new Mock<IExternalService>();
            mockExternalService
                .Setup(s => s.GetProductList())
                .Returns(Task.FromResult<List<ProductModel>>(new List<ProductModel>() {
                    new ProductModel() { Name = "ProductA", Price = 5.0, Quantity = 5 },
                    new ProductModel() { Name = "ProductB", Price = 2.5, Quantity = 2 },
                    new ProductModel() { Name = "ProductC", Price = 1, Quantity = 3 },
                }));

            mockExternalService
                .Setup(s => s.GetShopperHistory())
                .Returns(Task.FromResult<List<ShopperHistoryModel>>(new List<ShopperHistoryModel>() {
                    new ShopperHistoryModel()
                    {
                        CustomerId = 1,
                        Products = new List<ProductModel>()
                        {
                            new ProductModel() { Name = "ProductA", Price = 5.0, Quantity = 1 },
                            new ProductModel() { Name = "ProductB", Price = 2.5, Quantity = 2 },
                        }
                    },
                    new ShopperHistoryModel()
                    {
                        CustomerId = 2,
                        Products = new List<ProductModel>()
                        {
                            new ProductModel() { Name = "ProductB", Price = 5.0, Quantity = 3 },
                            new ProductModel() { Name = "ProductC", Price = 2.5, Quantity = 2 },
                        }
                    }
                }));

            _sortService = new SortService(mockExternalService.Object);
        }

        [TestMethod]
        public async Task Sort()
        {
            var products = await _sortService.Sort("Low");
            Assert.IsTrue(products[0].Name == "ProductC");

            products = await _sortService.Sort("High");
            Assert.IsTrue(products[0].Name == "ProductA");

            products = await _sortService.Sort("Ascending");
            Assert.IsTrue(products[0].Name == "ProductA");

            products = await _sortService.Sort("Descending");
            Assert.IsTrue(products[0].Name == "ProductC");

            products = await _sortService.Sort("Recommended");
            Assert.IsTrue(products[0].Name == "ProductB");
        }

        [TestCleanup]
        public void Cleanup()
        {

        }
    }
}
