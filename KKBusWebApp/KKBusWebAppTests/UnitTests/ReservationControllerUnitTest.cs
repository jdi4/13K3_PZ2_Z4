using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using KKBusWebApp.Controllers;
using Moq;
using System.Security.Claims;
using System.Security.Principal;
using System.Data.Entity;
using KKBusWebApp.Models;
using Effort.DataLoaders;
using System.Data.Common;
using System.Data.Entity.Core.EntityClient;
using System.Collections.Generic;

namespace KKBusWebAppTests.UnitTests
{
    [TestClass]
    public class ReservationControllerUnitTests
    {
        private kkbusDBEntities fakeDBContext;
        private ControllerContext mockControllerContext;

        [TestInitialize]
        public void Initialize()
        {
            // Asp.NET Identity Mock
            
            //var claim = new Claim("test1", "11");
            //identity.Claims = new List<Claim>() { claim };
            //var tempclaim = identity.Claims;
            //foreach (Claim c in tempclaim)
            //{
            //    identity.RemoveClaim(c);
            //}
            List<Claim> claims = new List<Claim>{
                new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", "test1"), 
                new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", "11")
            };

            var identity = new GenericIdentity("");
            identity.AddClaims(claims);
            var principal = new GenericPrincipal(identity, new[] { "CLIENT" });
            this.mockControllerContext = Mock.Of<ControllerContext>(cc => cc.HttpContext.User == principal);

            // Effort database fake
            //IDataLoader OSOBYloader = new Effort.DataLoaders.CsvDataLoader(@"\DB Test Data\OSOBY.csv");
            //IDataLoader KLIENCIloader = new Effort.DataLoaders.CsvDataLoader(@"\DB Test Data\KLIENCI.csv");
            IDataLoader loader = new Effort.DataLoaders.CsvDataLoader(@"D:\SQL Export t");

            //IDataLoader loader = new Effort.DataLoaders.EntityDataLoader("name=kkbusDBEntities");

            EntityConnection connection =
                Effort.EntityConnectionFactory.CreateTransient("name=kkbusDBEntities", loader);
            this.fakeDBContext = new kkbusDBEntities(connection);
        }


        [TestMethod]
        public void Return_IndexView()
        {
            //Arrange
            var controller = new ReservationController(fakeDBContext)
            {
                ControllerContext = mockControllerContext
            };

            //Act
            ViewResult result = controller.Index() as ViewResult;

            //Asert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void PassMessage_InViewBag_Index()
        {
            //Arrange
            var controller = new ReservationController(fakeDBContext)
            {
                ControllerContext = mockControllerContext
            };
            controller.TempData["ErrorMessage"] = "Test Error";

            //Act
            ViewResult result = controller.Index() as ViewResult;

            //Asert
            Assert.AreEqual("Test Error", result.ViewBag.Message);
        }
    }
}
