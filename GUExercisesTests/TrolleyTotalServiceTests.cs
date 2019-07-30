using GUExercises.Models;
using GUExercises.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace GUExercisesTests
{
    [TestClass]
    public class TrolleyTotalServiceTests
    {
        private TrolleyTotalService _trolleyTotalService;

        [TestInitialize]
        public void Initialize()
        {
            _trolleyTotalService = new TrolleyTotalService();
        }

        [TestMethod]
        public void GetTrolleyTotal_NoSpecial()
        {
            TrolleyModel trolleyModel = new TrolleyModel();
            trolleyModel.Products = new List<TrolleyProduct>();
            trolleyModel.Products.Add(new TrolleyProduct() { Name = "Product 0", Price = 14.6584559975464M });
            trolleyModel.Products.Add(new TrolleyProduct() { Name = "Product 1", Price = 6.25149348110496M });
            trolleyModel.Products.Add(new TrolleyProduct() { Name = "Product 2", Price = 13.6667721409662M });

            trolleyModel.Specials = new List<TrolleySpecial>();
            List<TrolleyProductQuantity> tpq1 = new List<TrolleyProductQuantity>();
            tpq1.Add(new TrolleyProductQuantity() { Name = "Product 0", Quantity = 5 });
            tpq1.Add(new TrolleyProductQuantity() { Name = "Product 1", Quantity = 5 });
            tpq1.Add(new TrolleyProductQuantity() { Name = "Product 2", Quantity = 2 });
            trolleyModel.Specials.Add(new TrolleySpecial() { Total = 8.78499156130719M, Quantities = tpq1 });

            List<TrolleyProductQuantity> tpq2 = new List<TrolleyProductQuantity>();
            tpq2.Add(new TrolleyProductQuantity() { Name = "Product 0", Quantity = 0 });
            tpq2.Add(new TrolleyProductQuantity() { Name = "Product 1", Quantity = 4 });
            tpq2.Add(new TrolleyProductQuantity() { Name = "Product 2", Quantity = 3 });
            trolleyModel.Specials.Add(new TrolleySpecial() { Total = 31.7466055797785M, Quantities = tpq2 });

            List<TrolleyProductQuantity> tpq3 = new List<TrolleyProductQuantity>();
            tpq3.Add(new TrolleyProductQuantity() { Name = "Product 0", Quantity = 1 });
            tpq3.Add(new TrolleyProductQuantity() { Name = "Product 1", Quantity = 9 });
            tpq3.Add(new TrolleyProductQuantity() { Name = "Product 2", Quantity = 5 });
            trolleyModel.Specials.Add(new TrolleySpecial() { Total = 29.3865395547186M, Quantities = tpq3 });

            trolleyModel.Quantities = new List<TrolleyProductQuantity>();
            trolleyModel.Quantities.Add(new TrolleyProductQuantity() { Name = "Product 0", Quantity = 5 });
            trolleyModel.Quantities.Add(new TrolleyProductQuantity() { Name = "Product 1", Quantity = 7 });
            trolleyModel.Quantities.Add(new TrolleyProductQuantity() { Name = "Product 2", Quantity = 1 });

            decimal total = _trolleyTotalService.GetTrolleyTotal(trolleyModel);
            Assert.AreEqual(130.71950649643292M, total);
        }

        [TestMethod]
        public void GetTrolleyTotal_WithSpecial()
        {
            TrolleyModel trolleyModel = new TrolleyModel();
            trolleyModel.Products = new List<TrolleyProduct>();
            trolleyModel.Products.Add(new TrolleyProduct() { Name = "Product 0", Price = 2 });
            trolleyModel.Products.Add(new TrolleyProduct() { Name = "Product 1", Price = 5 });

            trolleyModel.Specials = new List<TrolleySpecial>();
            List<TrolleyProductQuantity> tpq1 = new List<TrolleyProductQuantity>();
            tpq1.Add(new TrolleyProductQuantity() { Name = "Product 0", Quantity = 3 });
            tpq1.Add(new TrolleyProductQuantity() { Name = "Product 1", Quantity = 0 });
            trolleyModel.Specials.Add(new TrolleySpecial() { Total = 5, Quantities = tpq1 });

            List<TrolleyProductQuantity> tpq2 = new List<TrolleyProductQuantity>();
            tpq2.Add(new TrolleyProductQuantity() { Name = "Product 0", Quantity = 1 });
            tpq2.Add(new TrolleyProductQuantity() { Name = "Product 1", Quantity = 2 });;
            trolleyModel.Specials.Add(new TrolleySpecial() { Total = 10, Quantities = tpq2 });

            trolleyModel.Quantities = new List<TrolleyProductQuantity>();
            trolleyModel.Quantities.Add(new TrolleyProductQuantity() { Name = "Product 0", Quantity = 3 });
            trolleyModel.Quantities.Add(new TrolleyProductQuantity() { Name = "Product 1", Quantity = 2 });

            decimal total = _trolleyTotalService.GetTrolleyTotal(trolleyModel);
            Assert.AreEqual(14, total);
        }

        [TestCleanup]
        public void Cleanup()
        {

        }
    }
}
