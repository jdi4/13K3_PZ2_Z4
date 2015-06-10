using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KKBusWebApp.Models;
using System.Web.Mvc;
using KKBusWebApp.Controllers;

namespace KKBusWebAppTests.UnitTests
{
    [TestClass]
    public class TrasyControllerUnitTests
    {
        private kkbusDBEntities dummyDBContext;

        [TestInitialize]
        public void Initialize()
        {
            //this.mockControllerContext = TestsEnvironment.SetupControllerContextUserIdentity(new[] { "CLIENT" });
            this.dummyDBContext = TestsEnvironment.SetupDatabase();
        }

        [TestMethod]
        public void Return_IndexView()
        {
            //Arrange
            var controller = new TrasyController(dummyDBContext);

            //Act
            ViewResult result = controller.Index() as ViewResult;

            //Asert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Return_DetailsView()
        {
            //Arrange
            var controller = new TrasyController(dummyDBContext);

            TRASA t = new TRASA()
            {
                TRA_CENA = 5,
                TRA_CZAS_PRZEJAZDU = 2,
                PRZY_ID = 1,
                KUR_ID = 1,
                TRA_KOLEJNOSC = 1
            };

            dummyDBContext.TRASA.Add(t);
            dummyDBContext.SaveChanges();

            int traId = t.TRA_ID;

            //Act
            ViewResult result = controller.Details(traId) as ViewResult;
            var resultData = result.ViewData.Model as TRASA;

            //Asert
            Assert.IsNotNull(result);
            Assert.AreEqual(resultData, t);
        }
    }
}
