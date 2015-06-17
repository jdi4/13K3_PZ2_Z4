using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KKBusWebApp.Models
{
    [MetadataType(typeof(PRZEJAZDYMetadata))]
    public partial class PRZEJAZDY
    { }

    public class PRZEJAZDYMetadata
    {
        [Display(Name = "ID przejazdu")]
        public int PRZ_ID { get; set; }

        [Display(Name = "ID kursu")]
        public int KUR_ID { get; set; }

        [Display(Name = "ID pracownika")]
        public int PRA_ID { get; set; }

        [Display(Name = "ID kierowcy")]
        public int KIE_ID { get; set; }

        [Display(Name = "W trakcie")]
        public bool PRZ_AKTYWNY { get; set; }

        [Display(Name = "Czas odjazdu")]
        public System.DateTime PRZ_ODJAZD { get; set; }
    }
}