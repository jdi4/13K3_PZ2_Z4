using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KKBusWebApp.Models;
using System.Web.Mvc;
using KKBusWebApp.Areas.Admin.Controllers;

namespace KKBusWebAppTests.UnitTests
{
    [TestClass]
    public class EmployeesControllerUnitTests
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
            var controller = new EmployeesController(dummyDBContext);

            //Act
            ViewResult result = controller.Index() as ViewResult;

            //Asert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Return_DetailsView()
        {
            //Arrange
            var controller = new EmployeesController(dummyDBContext);

            PRACOWNICY t = new PRACOWNICY()
            {
                OSO_ID =1,
                PRA_UPRAWNIENIA = "WORKER",
            };

            dummyDBContext.PRACOWNICY.Add(t);
            dummyDBContext.SaveChanges();

            int traId = t.PRA_ID;

            //Act
            ViewResult result = controller.Details(traId) as ViewResult;
            var resultData = result.ViewData.Model as PRACOWNICY;

            //Asert
            Assert.IsNotNull(result);
            Assert.AreEqual(resultData, t);
        }
    }
}
