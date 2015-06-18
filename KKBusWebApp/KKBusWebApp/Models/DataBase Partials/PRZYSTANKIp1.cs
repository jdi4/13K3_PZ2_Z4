using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KKBusWebApp.Models
{
    [MetadataType(typeof(PRZYSTANKIMetadata))]
    public partial class PRZYSTANKI
    { }

    public class PRZYSTANKIMetadata
    {
        [Display(Name = "Nazwa")]
        public string PRZY_NAZWA { get; set; }

        [Display(Name = "Długość geograficzna")]
        public double PRZY_GPS_LONGITUDE { get; set; }

        [Display(Name = "Szerokość geograficzna")]
        public double PRZY_GPS_LATITUDE { get; set; }

        [Display(Name = "Na żądanie?")]
        public byte[] PRZY_ZADANIE { get; set; }
    }
}