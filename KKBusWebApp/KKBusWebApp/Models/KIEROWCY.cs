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
    
    public partial class KIEROWCY
    {
        public KIEROWCY()
        {
            this.PRZEJAZDY = new HashSet<PRZEJAZDY>();
            this.TANKOWANIA = new HashSet<TANKOWANIA>();
        }
    
        public int KIE_ID { get; set; }
        public int OSO_ID { get; set; }
    
        public virtual OSOBY OSOBY { get; set; }
        public virtual ICollection<PRZEJAZDY> PRZEJAZDY { get; set; }
        public virtual ICollection<TANKOWANIA> TANKOWANIA { get; set; }
    }
}
