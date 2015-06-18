using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KKBusWebApp.Models
{

    [MetadataType(typeof(TANKOWANIAMetadata))]
    public partial class TANKOWANIA
    { }

    public class TANKOWANIAMetadata
    {
        [Display(Name = "ID tankowania")]
        public int TAN_ID { get; set; }

        [Display(Name = "ID kierowcy")]
        public int KIE_ID { get; set; }

        [Display(Name = "ID pojazdu")]
        public int POJ_ID { get; set; }

        [Display(Name = "Ilość zatankowanego paliwa")]
        public double TAN_ILOSC { get; set; }

        [Display(Name = "Kwota zapłacona za paliwo")]
        public double TAN_KWOTA { get; set; }

        [Display(Name = "Przewidywana data kolejnego tankowania")]
        public System.DateTime TAN_NASTEPNE { get; set; }

        [Display(Name = "km przejechanych od ostatniego tankowania")]
        public int TAN_PRZEJECHANO { get; set; }
    }
}