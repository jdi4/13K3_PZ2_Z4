using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KKBusWebApp.Models
{
    public class LPHomeViewModel
    {
        public string UserName { get; set; }

        public int ClientID { get; set; }

        public int LPPoints { get; set; }

        public IEnumerable<DOSTEPNE_NAGRODY> AvaiablePrizes { get; set; }

        public IEnumerable<NAGRODY_KLIENCI> UserPrizes { get; set; }
    }
}