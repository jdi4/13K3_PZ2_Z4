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

        public IEnumerable<TicketTypeViewModel> TicketsTypes { get; set; }

        public SelectList TicketsTypesDropDownList { get; set; }

        public string PersonName { get; set; }
    }

    public class TicketTypeViewModel //: RODZAJE_BILETOW
    {
        public int TicketID { get; set; }

        public string TicketName { get; set; }

        public int TicketsNumber { get; set; }
    }
}