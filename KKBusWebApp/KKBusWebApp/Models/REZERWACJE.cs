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
    
    public partial class REZERWACJE
    {
        public REZERWACJE()
        {
            this.ULGI_REZERWACJA = new HashSet<ULGI_REZERWACJA>();
        }
    
        public int REZ_ID { get; set; }
        public int KLI_ID { get; set; }
        public int PRZ_ID { get; set; }
        public double REZ_CENA { get; set; }
        public byte[] REZ_WYKORZYSTANA { get; set; }
        public int REZ_DOKUMENT { get; set; }
    
        public virtual KLIENCI KLIENCI { get; set; }
        public virtual PRZEJAZDY PRZEJAZDY { get; set; }
        public virtual ICollection<ULGI_REZERWACJA> ULGI_REZERWACJA { get; set; }
    }
}
