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
    
    public partial class ULGI_REZERWACJA
    {
        public int ULR_ID { get; set; }
        public int REZ_ID { get; set; }
        public int ROD_ID { get; set; }
        public int ULR_ILOSC { get; set; }
    
        public virtual REZERWACJE REZERWACJE { get; set; }
        public virtual RODZAJE_BILETOW RODZAJE_BILETOW { get; set; }
    }
}
