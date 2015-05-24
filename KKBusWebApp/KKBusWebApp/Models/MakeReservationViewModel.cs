using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KKBusWebApp.Models
{
    public class MakeReservationViewModel
    {
        public string CourseName { get; set; }

        public REZERWACJE Reservation { get; set; }

        public IEnumerable<RODZAJE_BILETOW> TicketsTypes { get; set; }

        public SelectList TicketsTypesList { get; set; }

        public string Name { get; set; }
    }
}