using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstate.Controllers;
using RealEstate.Data;
using RealEstate.Models;

namespace RealEstateTests
{
    [TestClass]
    public class ForSaleControllerTests
    {
        // db var at class level for use in all tests
        private ApplicationDbContext context;
        ForSalesController controller;

        //Test Initialize cladd that holds test data for Houses
        [TestInitialize]
        public void TestInitialize()
        {
            // must instantiate in memory db to pass as a dependency when creating an instance of ProductsController
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            context = new ApplicationDbContext(options);

            // seed the db before passing it to controller
            var city = new City { CityId = 420, Name = "Test ", AreaCode = "L4N" };
            context.Add(city);

            for (var i = 100; i < 111; i++)
            {
                var forSale = new ForSale { ForSaleId = i, Name = "House" + i.ToString(), CityId = 312, City = city, Price = i + 10, Description = "Big", Rooms = 2, SqFootage = 34 };
                context.Add(forSale);
            }

            var extraProduct = new ForSale { ForSaleId = 123, Name = "Cardboard Box", CityId = 812, City = city, Price = 69, Description = "Small", Rooms = 5, SqFootage = 94 };
            context.Add(extraProduct);
            context.SaveChanges();

            controller = new ForSalesController(context);
        }

        #region "Index"
        [TestMethod]
        public void IndexLoadView()
        {
            // act
            var result = (ViewResult)controller.Index().Result;

            // assert
            Assert.AreEqual("Index", result.ViewName);
        }   
    }
}
