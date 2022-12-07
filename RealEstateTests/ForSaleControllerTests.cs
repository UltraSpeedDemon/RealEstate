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

        //Test Initialize
        [TestInitialize]
        public void TestInitialize()
        {
            // must instantiate in memory db to pass as a dependency when creating an instance of ProductsController
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            context = new ApplicationDbContext(options);

            controller = new ForSalesController(context);
        }
    }
}