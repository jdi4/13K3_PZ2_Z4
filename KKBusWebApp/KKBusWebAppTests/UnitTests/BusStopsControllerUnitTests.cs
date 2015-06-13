using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KKBusWebApp.Models;
using System.Web.Mvc;
using KKBusWebApp.Areas.Admin.Controllers;

namespace KKBusWebAppTests.UnitTests
{
    [TestClass]
    public class BusStopsControllerUnitTests
    {
        private kkbusDBEntities dummyDBContext;
        private ControllerContext mockControllerContext;

        [TestInitialize]
        public void Initialize()
        {
            this.mockControllerContext = TestsEnvironment.SetupControllerContextUserIdentity(new[] { "OWNER" });
            this.dummyDBContext = TestsEnvironment.SetupDatabase();
        }

        [TestMethod]
        public void Return_IndexView()
        {
            //Arrange
            var controller = new BusStopsController(dummyDBContext);

            //Act
            ViewResult result = controller.Index() as ViewResult;

            //Asert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Return_DetailsView()
        {
            //Arrange
            var controller = new BusStopsController(dummyDBContext);

            PRZYSTANKI t = new PRZYSTANKI()
            {
                PRZY_GPS_LATITUDE = 20.0,
                PRZY_GPS_LONGITUDE = 20.0,
                PRZY_NAZWA = "test",
                PRZY_ZADANIE = new byte[1] { 1 }
            };

            dummyDBContext.PRZYSTANKI.Add(t);
            dummyDBContext.SaveChanges();

            int traId = t.PRZY_ID;

            //Act
            ViewResult result = controller.Details(traId) as ViewResult;
            var resultData = result.ViewData.Model as PRZYSTANKI;

            //Asert
            Assert.IsNotNull(result);
            Assert.AreEqual(resultData, t);
        }
    }
}
