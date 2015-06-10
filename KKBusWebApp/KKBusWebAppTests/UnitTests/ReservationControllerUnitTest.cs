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

        [TestMethod]
        public void Return_AvaiableReservationsView()
        {
            //Arrange
            var controller = new ReservationController(dummyDBContext)
            {
                ControllerContext = mockControllerContext
            };

            PRZEJAZDY p = new PRZEJAZDY()
            {
                PRZ_ODJAZD = DateTime.Now.AddDays(2),
                KUR_ID = 1,
                KIE_ID = 1,
                PRA_ID = 1
            };

            dummyDBContext.PRZEJAZDY.Add(p);
            dummyDBContext.SaveChanges();

            //Act
            ViewResult result = controller.AvaiableReservations() as ViewResult;
            List<PRZEJAZDY> resultData = result.ViewData.Model as List<PRZEJAZDY>;

            //Asert
            Assert.IsNotNull(result);
            Assert.AreEqual(resultData.Last(), p);
        }

        [TestMethod]
        public void Redirect_WhenToLateTo_Cancel()
        {
            //Arrange
            var controller = new ReservationController(dummyDBContext)
            {
                ControllerContext = mockControllerContext
            };

            PRZEJAZDY p = new PRZEJAZDY()
            {
                PRZ_ODJAZD = DateTime.Now.AddHours(20),
                KUR_ID = 1,
                KIE_ID = 1,
                PRA_ID = 1
            };

            dummyDBContext.PRZEJAZDY.Add(p);
            dummyDBContext.SaveChanges();

            int przId = p.PRZ_ID;

            REZERWACJE r = new REZERWACJE()
            {
                REZ_CENA = 10,
                REZ_DOKUMENT = 0,
                REZ_WYKORZYSTANA = new byte[1] { 0 },
                PRZ_ID = przId,
                KLI_ID = 1
            };

            dummyDBContext.REZERWACJE.Add(r);
            dummyDBContext.SaveChanges();

            int rezId = r.REZ_ID;

            //Act
            var result = controller.Cancel(rezId) as RedirectToRouteResult;

            //Asert
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void Return_CancelView()
        {
            //Arrange
            var controller = new ReservationController(dummyDBContext)
            {
                ControllerContext = mockControllerContext
            };

            PRZEJAZDY p = new PRZEJAZDY()
            {
                PRZ_ODJAZD = DateTime.Now.AddHours(25),
                KUR_ID = 1,
                KIE_ID = 1,
                PRA_ID = 1
            };

            dummyDBContext.PRZEJAZDY.Add(p);
            dummyDBContext.SaveChanges();

            int przId = p.PRZ_ID;

            REZERWACJE r = new REZERWACJE()
            {
                REZ_CENA = 10,
                REZ_DOKUMENT = 0,
                REZ_WYKORZYSTANA = new byte[1] { 0 },
                PRZ_ID = przId,
                KLI_ID = 1
            };

            dummyDBContext.REZERWACJE.Add(r);
            dummyDBContext.SaveChanges();

            int rezId = r.REZ_ID;

            //Act
            var result = controller.Cancel(rezId) as ViewResult;
            var resultData = result.ViewData.Model as REZERWACJE;

            //Asert
            Assert.IsNotNull(result);
            Assert.AreEqual(resultData, r);
        }
    }
}
