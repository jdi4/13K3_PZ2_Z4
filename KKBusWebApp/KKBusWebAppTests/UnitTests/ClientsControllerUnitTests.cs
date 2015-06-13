using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KKBusWebApp.Models;
using KKBusWebApp.Areas.Admin.Controllers;
using System.Web.Mvc;

namespace KKBusWebAppTests.UnitTests
{
    [TestClass]
    public class ClientsControllerUnitTests
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
            var controller = new ClientsController(dummyDBContext);

            //Act
            ViewResult result = controller.Index() as ViewResult;

            //Asert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Return_DetailsView()
        {
            //Arrange
            var controller = new ClientsController(dummyDBContext);

            KLIENCI t = new KLIENCI()
            {
                KLI_EMAIL = "test",
                KLI_PUNKTY = 1,
                KLI_ZABLOKOWANY = new byte[1] {0},
                OSO_ID = 1
            };

            dummyDBContext.KLIENCI.Add(t);
            dummyDBContext.SaveChanges();

            int traId = t.KLI_ID;

            //Act
            ViewResult result = controller.Details(traId) as ViewResult;
            var resultData = result.ViewData.Model as KLIENCI;

            //Asert
            Assert.IsNotNull(result);
            Assert.AreEqual(resultData, t);
        }
    }
}
