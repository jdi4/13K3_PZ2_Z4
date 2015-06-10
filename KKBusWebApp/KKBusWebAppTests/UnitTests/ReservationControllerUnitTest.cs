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
using System.Linq;

namespace KKBusWebAppTests.UnitTests
{
    [TestClass]
    public class ReservationControllerUnitTests
    {
        private kkbusDBEntities dummyDBContext;
        private ControllerContext mockControllerContext;

        [TestInitialize]
        public void Initialize()
        {
            this.mockControllerContext = TestsEnvironment.SetupControllerContextUserIdentity(new[] { "CLIENT" });
            this.dummyDBContext = TestsEnvironment.SetupDatabase();
        }

        [TestMethod]
        public void Return_IndexView()
        {
            //Arrange
            var controller = new ReservationController(dummyDBContext)
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
            var controller = new ReservationController(dummyDBContext)
            {
                ControllerContext = mockControllerContext
            };
            controller.TempData["ErrorMessage"] = "Test Error";

            //Act
            ViewResult result = controller.Index() as ViewResult;

            //Asert
            Assert.AreEqual("Test Error", result.ViewBag.Message);
        }

        [TestMethod]
        public void Redirect_WhenToLateTo_MakeReservation()
        {
            //Arrange
            var controller = new ReservationController(dummyDBContext)
            {
                ControllerContext = mockControllerContext
            };

            PRZEJAZDY p1 = new PRZEJAZDY()
            {
                PRZ_ODJAZD = DateTime.Now.AddHours(2), // nie można odwołać
                KUR_ID = 1,
                KIE_ID = 1,
                PRA_ID = 1
            };
            PRZEJAZDY p2 = new PRZEJAZDY()
            {
                PRZ_ODJAZD = DateTime.Now.AddHours(6), // można odwołac
                KUR_ID = 1,
                KIE_ID = 1,
                PRA_ID = 1
            };
            dummyDBContext.PRZEJAZDY.Add(p1);
            dummyDBContext.PRZEJAZDY.Add(p2);
            dummyDBContext.SaveChanges();

            List<TicketTypeViewModel> tickets = new List<TicketTypeViewModel>()
            {
                new TicketTypeViewModel() 
                {
                    TicketID = 1,
                    TicketName = "TestTicket",
                    TicketsNumber = 1
                }
            };

            dummyDBContext.PRZEJAZDY.Load();
            dummyDBContext.KURSY.Load();

            //Act
            var result1 = controller.MakeReservation(p1.PRZ_ID, tickets) as RedirectToRouteResult;
            var result2 = controller.MakeReservation(p2.PRZ_ID, tickets) as RedirectToRouteResult;
            
            //Asert
            Assert.AreEqual("AvaiableReservations", result1.RouteValues["action"]);
            Assert.AreEqual("Index", result2.RouteValues["action"]);
        }
    }
}
