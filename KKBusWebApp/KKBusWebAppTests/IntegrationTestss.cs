using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KKBusWebApp.Models;
using System.Data.Entity;
using System.Linq;

namespace KKBusWebAppTests
{
    [TestClass]
    public class IntegrationTests
    {
        protected kkbusDBEntities db;
        private DbContextTransaction transaction;

        [TestInitialize]
        public void Initialize()
        {
            this.db = new kkbusDBEntities();
            this.transaction = db.Database.BeginTransaction();
        }

        [TestCleanup]
        public void Cleanup()
        {
            transaction.Rollback();
            transaction.Dispose();
            db.Dispose();
        }

        [TestMethod]
        [TestCategory("IntegrationTests")]
        public void AddVehicleTest()
        {
            POJAZDY p = new POJAZDY()
            {
                POJ_DATA_PRODUKCJI = DateTime.Now,
                POJ_MARKA = "testowa marka",
                POJ_MIEJSCA = 10,
                POJ_SILNIK = "test",
                POJ_SPRAWNY = new byte[1] { 1 },
                POJ_UBEZPIECZONY_DO = DateTime.Now
            };

            db.POJAZDY.Add(p);
            db.SaveChanges();

            var testp = db.POJAZDY.FirstOrDefault(poj => poj.POJ_MARKA == "testowa marka");
            Assert.IsNotNull(testp);
            Assert.AreEqual(p.POJ_ID, testp.POJ_ID);
        }

        [TestMethod]
        [TestCategory("IntegrationTests")]
        public void AddPrizeTest()
        {
            DOSTEPNE_NAGRODY p = new DOSTEPNE_NAGRODY()
            {
                NAG_AKTYWNA = new byte[1] { 1 },
                NAG_NAZWA = "test",
                NAG_PUNKTY = 10,
                NAG_SCIEZKA_OBRAZKA = "test",
            };

            db.DOSTEPNE_NAGRODY.Add(p);
            db.SaveChanges();

            var item = db.DOSTEPNE_NAGRODY.FirstOrDefault(i => i.NAG_NAZWA == "test");
            Assert.IsNotNull(item);
            Assert.AreEqual(p.NAG_ID, item.NAG_ID);
        }

        [TestMethod]
        [TestCategory("IntegrationTests")]
        public void DeleteTickeTypeTest()
        {
            RODZAJE_BILETOW t = new RODZAJE_BILETOW()
            {
                ROD_NAZWA = "test",
                ROD_WIEK = 10,
                ROD_ZNIZKA = 30
            };

            db.RODZAJE_BILETOW.Add(t);
            db.SaveChanges();

            RODZAJE_BILETOW tt = db.RODZAJE_BILETOW.FirstOrDefault(x => x.ROD_ID == t.ROD_ID);

            int tid = tt.ROD_ID;
            int number = db.RODZAJE_BILETOW.Count();

            db.RODZAJE_BILETOW.Remove(tt);
            db.SaveChanges();

            var item = db.RODZAJE_BILETOW.FirstOrDefault(i => i.ROD_ID == tid);
            Assert.IsNull(item);
            Assert.AreEqual(db.RODZAJE_BILETOW.Count(), number - 1);
        }
    }
}
