using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace KKBusWebApp.Models
{
    public partial class kkbusDBEntities
    {
        public kkbusDBEntities(string connectionString)
            : base(connectionString)
        {

        }

        public kkbusDBEntities(DbConnection connection)
            : base(connection, true)
        {

        }
    }
}