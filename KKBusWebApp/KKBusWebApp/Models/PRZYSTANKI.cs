//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KKBusWebApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PRZYSTANKI
    {
        public PRZYSTANKI()
        {
            this.TRASA = new HashSet<TRASA>();
        }
    
        public int PRZY_ID { get; set; }
        public string PRZY_NAZWA { get; set; }
        public double PRZY_GPS_LONGITUDE { get; set; }
        public double PRZY_GPS_LATITUDE { get; set; }
        public byte[] PRZY_ZADANIE { get; set; }
    
        public virtual ICollection<TRASA> TRASA { get; set; }
    }
}
