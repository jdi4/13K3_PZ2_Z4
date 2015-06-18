using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KKBusWebApp.Models
{
    [MetadataType(typeof(POJAZDYMetadata))]
    public partial class POJAZDY
    { }

    public class POJAZDYMetadata
    {
        [Display(Name = "ID pojazdu")]
        public int POJ_ID { get; set; }

        [Display(Name = "Marka")]
        public string POJ_MARKA { get; set; }

        [Display(Name = "Rodzaj silnika")]
        public string POJ_SILNIK { get; set; }

        [Display(Name = "Data produkcji")]
        public System.DateTime POJ_DATA_PRODUKCJI { get; set; }

        [Display(Name = "Ubezpieczony do")]
        public Nullable<System.DateTime> POJ_UBEZPIECZONY_DO { get; set; }

        [Display(Name = "Sprawny")]
        public byte[] POJ_SPRAWNY { get; set; }

        [Display(Name = "Adnotacje")]
        public string POJ_ADNOTACJE { get; set; }

        [Display(Name = "Liczba miejsc")]
        public int POJ_MIEJSCA { get; set; }
    }
}