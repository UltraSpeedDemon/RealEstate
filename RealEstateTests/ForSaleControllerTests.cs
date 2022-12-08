using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
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
        private ApplicationDbContext context; //MOCK DATABASE
        ForSalesController controller;

        //Test Initialize cladd that holds test data for Houses

       
        [TestInitialize]
        public void TestInitialize()
        {
            //Instantation for the DbContext and Mock Database
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            context = new ApplicationDbContext(options);
            
            //adds information from the models to the mock database
            var city = new City { CityId = 420, Name = "Test ", AreaCode = "L4N" };
            context.Add(city);

            for (var i = 100; i < 111; i++)
            {
                var forSale = new ForSale { ForSaleId = i, Name = "House" + i.ToString(), CityId = 104, City = city, Price = i + 10, Description = "Big", Rooms = 2, SqFootage = 34 };
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
       
        [TestMethod]
        public void IndexLoadsForSale()
        {
            // act
            var result = (ViewResult)controller.Index().Result;
            List<ForSale> model = (List<ForSale>)result.Model;

            // assert
            CollectionAssert.AreEqual(context.ForSale.OrderBy(p => p.Name).ToList(), model);
        }
        #endregion

        #region "Details"
        [TestMethod]
        public void DetailsWithNoIDLoads404()
        {
            // act
            var result = (ViewResult)controller.Details(null).Result;

            // assert 
            Assert.AreEqual("404", result.ViewName); //Error 404 Page
        }

        [TestMethod]
        public void DetailsNoHouseTableLoads404()
        {
            // arrange
            context.ForSale = null;

            // act
            var result = (ViewResult)controller.Details(null).Result;

            // assert 
            Assert.AreEqual("404", result.ViewName);
        }

        [TestMethod]
        public void DetailsInvalidWillLoad404()
        {
            // act
            var result = (ViewResult)controller.Details(23).Result;

            // assert 
            Assert.AreEqual("404", result.ViewName);
        }

        [TestMethod]
        public void DetailsValidIDLoadsDetails()
        {
            // act
            var result = (ViewResult)controller.Details(104).Result;

            // assert 
            Assert.AreEqual("Details", result.ViewName);
        }

        [TestMethod]
        public void DetailsValidIDLoadProperty()
        {
            // act
            var result = (ViewResult)controller.Details(104).Result;

            // assert 
            Assert.AreEqual(context.ForSale.Find(104), result.Model);
        }
        #endregion
        #region "Create POST"
        [TestMethod]
        public void CreatePOSTValidIDLoadView()
        {
            //arrange
            var forSale = new ForSale { ForSaleId = 1, Name = "House", Price = 10, Description = "Big", Photo = "ahas", Rooms = 2, SqFootage = 34, CityId = 1 };

            // Act
            var result = (RedirectToActionResult)controller.Create(forSale).Result;

            // Assert
            Assert.AreEqual("Index", result.ActionName);
        }

        [TestMethod]
        public void CreatePOSTInvalidIDLoadView()
        {
            //arrange
            var forSale = new ForSale { ForSaleId = 1, Name = "House", Price = 10, Description = "Big", Photo = "ahas", Rooms = 2, SqFootage = 34, CityId = 1 };
            controller.ModelState.AddModelError("Name", "Name Test");

            // Act
            var result = (ViewResult)controller.Create(forSale).Result;

            // Assert
            Assert.AreEqual("Create", result.ViewName);
        }
        #endregion
        #region "Create"

        //Create 
        // GET: ForSales/Create
        [TestMethod]
        public void CreateValidIDLoadView()
        {
            // Act
            var result = (ViewResult)controller.Create();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Create", result.ViewName);
        }
        #endregion

        #region "Edit"
        [TestMethod]
        public void EditValidIDLoadView()
        {
            // Act
            var result = (ViewResult)controller.Edit(104).Result;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Edit", result.ViewName);
        }
        [TestMethod]
        public void EditWithNoIDLoads404()
        {
            // act
            var result = (ViewResult)controller.Edit(null).Result;

            // assert 
            Assert.AreEqual("404", result.ViewName); //Error 404 Page
        }

        [TestMethod]
        public void EditNoHomeTableLoads404()
        {
            // arrange
            context.ForSale = null;

            // act
            var result = (ViewResult)controller.Edit(null).Result;

            // assert 
            Assert.AreEqual("404", result.ViewName);
        }

        [TestMethod]
        public void EditInvalidWillLoad404()
        {
            // act
            var result = (ViewResult)controller.Edit(23).Result;

            // assert 
            Assert.AreEqual("404", result.ViewName);
        }

        [TestMethod]
        public void EditValidIDLoadProperty()
        {
            // act
            var result = (ViewResult)controller.Edit(104).Result;

            // assert 
            Assert.AreEqual(context.ForSale.Find(104), result.Model);
        }
        #endregion
        #region "EditPOST"
        [TestMethod]
        public void EditPOSTInValidLoadView404()
        {
            //arrange
            var forSale = new ForSale { ForSaleId = 1, Name = "House", Price = 10, Description = "Big", Photo = "ahas", Rooms = 2, SqFootage = 34, CityId = 1 };

            // Act
            var result = (ViewResult)controller.Edit(1, forSale).Result;

            // Assert
            Assert.AreEqual("404", result.ViewName);
        }
        [TestMethod]
        public void EditPOSTInValidIDLoadView404()
        {
            //arrange
            var forSale = new ForSale { ForSaleId = 3, Name = "House", Price = 10, Description = "Big", Photo = "ahas", Rooms = 2, SqFootage = 34, CityId = 1 };

            // Act
            var result = (ViewResult)controller.Edit(5, forSale).Result;

            // Assert
            Assert.AreEqual("404", result.ViewName);
        }

        [TestMethod]
        public void EditPOSTInvalidIDLoadView()
        {
            //arrange
            var forSale = new ForSale { ForSaleId = 1, Name = "House", Price = 10, Description = "Big", Photo = "ahas", Rooms = 2, SqFootage = 34, CityId = 1 };
            controller.ModelState.AddModelError("Name", "Name Test");

            // Act
            var result = (ViewResult)controller.Edit(1, forSale).Result;

            // Assert
            Assert.AreEqual("Edit", result.ViewName);
        }
        #endregion
        #region "Delete"
        [TestMethod]
        public void DeleteValidIDLoadView()
        {
            // Act
            var result = (ViewResult)controller.Delete(104).Result;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Delete", result.ViewName);
        }
        [TestMethod]
        public void DeleteWithNoIDLoads404()
        {
            // act
            var result = (ViewResult)controller.Delete(null).Result;

            // assert 
            Assert.AreEqual("404", result.ViewName); //Error 404 Page
        }

        [TestMethod]
        public void DeleteNoHouseTableLoads404()
        {
            // arrange
            context.ForSale = null;

            // act
            var result = (ViewResult)controller.Delete(null).Result;

            // assert 
            Assert.AreEqual("404", result.ViewName);
        }

        [TestMethod]
        public void DeleteInvalidWillLoad404()
        {
            // act
            var result = (ViewResult)controller.Delete(23).Result;

            // assert 
            Assert.AreEqual("404", result.ViewName);
        }

        [TestMethod]
        public void DeleteValidIDLoadProperty()
        {
            // act
            var result = (ViewResult)controller.Delete(104).Result;

            // assert 
            Assert.AreEqual(context.ForSale.Find(104), result.Model);
        }
        #endregion
        #region "Delete Confirmed"
        [TestMethod]
        public void DeleteConfirmedValidIDLoadView() //Does not Work. Unable to implement it. Partially Covered.
        {
            //Act
            var result = (RedirectToActionResult)controller.DeleteConfirmed(1).Result;

            //Assert
            Assert.AreEqual("Index", result.ActionName);

        }
        #endregion


    }
}
