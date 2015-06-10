using Effort.DataLoaders;
using KKBusWebApp.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.EntityClient;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace KKBusWebAppTests
{
    public static class TestsEnvironment
    {
        public static kkbusDBEntities SetupDatabase()
        {
            // Effort database fake
            string datapath = AppDomain.CurrentDomain.BaseDirectory + KKBusWebAppTests.Properties.Resources.DBTestData;
            IDataLoader loader = new Effort.DataLoaders.CsvDataLoader(datapath);
            EntityConnection connection =
                Effort.EntityConnectionFactory.CreateTransient("name=kkbusDBEntities", loader);
            return new kkbusDBEntities(connection);
        }

        public static ControllerContext SetupControllerContextUserIdentity(string[] roles)
        {
            // Asp.NET Identity Mock
            List<Claim> claims = new List<Claim>{
                new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", "test1"), 
                new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", "11")
            };

            var identity = new GenericIdentity("");
            identity.AddClaims(claims);
            var principal = new GenericPrincipal(identity, roles);
            return Mock.Of<ControllerContext>(cc => cc.HttpContext.User == principal);
        }
    }
}
