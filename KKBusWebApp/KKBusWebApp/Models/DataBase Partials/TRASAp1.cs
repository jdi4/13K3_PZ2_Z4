using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KKBusWebApp.Models
{

    [MetadataType(typeof(TRASAMetadata))]
    public partial class TRASA
    { }

    public class TRASAMetadata
    {
        [Display(Name = "Id Trasy")]
        public int TRA_ID { get; set; }

        [Display(Name = "Id Kursu")]
        public int KUR_ID { get; set; }

        [Display(Name = "Id Przystanku")]
        public int PRZY_ID { get; set; }

        [Display(Name = "Kolejność przystanku na trasie")]
        public int TRA_KOLEJNOSC { get; set; }

        [Display(Name = "Czas przejazdu",
            Description = "(od poprzedniego przystanku)")]
        public int TRA_CZAS_PRZEJAZDU { get; set; }

        [Display(Name = "Cena przejazdu",
            Description = "(od poprzedniego przystanku)")]
        [DisplayFormat(DataFormatString = "{0:#.##} zł")]
        public double TRA_CENA { get; set; }
    }
}