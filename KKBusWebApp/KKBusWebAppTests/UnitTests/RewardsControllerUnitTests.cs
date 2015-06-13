using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KKBusWebApp.Models;
using System.Web.Mvc;
using KKBusWebApp.Areas.Admin.Controllers;

namespace KKBusWebAppTests.UnitTests
{
    [TestClass]
    public class RewardsControllerUnitTests
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
            var controller = new RewardsController(dummyDBContext);

            //Act
            ViewResult result = controller.Index() as ViewResult;

            //Asert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Return_DetailsView()
        {
            //Arrange
            var controller = new RewardsController(dummyDBContext);

            DOSTEPNE_NAGRODY t = new DOSTEPNE_NAGRODY()
            {
                NAG_AKTYWNA = new byte[1] { 1 },
                NAG_NAZWA = "test",
                NAG_PUNKTY = 10,
                NAG_SCIEZKA_OBRAZKA = "test",
            };

            dummyDBContext.DOSTEPNE_NAGRODY.Add(t);
            dummyDBContext.SaveChanges();

            int traId = t.NAG_ID;

            //Act
            ViewResult result = controller.Details(traId) as ViewResult;
            var resultData = result.ViewData.Model as DOSTEPNE_NAGRODY;

            //Asert
            Assert.IsNotNull(result);
            Assert.AreEqual(resultData, t);
        }
    }
}
