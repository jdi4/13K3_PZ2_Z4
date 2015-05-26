using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KKBusWebApp.Models
{
    public class MakeReservationViewModel
    {
        public int CourseId { get; set; }

        public string CourseName { get; set; }

        public REZERWACJE Reservation { get; set; }

        //public IEnumerable<RODZAJE_BILETOW> TicketsTypes { get; set; }

        public IEnumerable<TicketType> TicketsTypes { get; set; }

        public SelectList TicketsTypesDropDownList { get; set; }

        public string Name { get; set; }
    }

    public class TicketType
    {
        public int TicketID { get; set; }

        public string TicketName { get; set; }

        public int TicketsNumber { get; set; }
    }
}