﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class kkbusDBEntities : DbContext
    {
        public kkbusDBEntities()
            : base("name=kkbusDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CZAS_PRACY> CZAS_PRACY { get; set; }
        public virtual DbSet<DOSTEPNE_NAGRODY> DOSTEPNE_NAGRODY { get; set; }
        public virtual DbSet<KIEROWCY> KIEROWCY { get; set; }
        public virtual DbSet<KLIENCI> KLIENCI { get; set; }
        public virtual DbSet<KURSY> KURSY { get; set; }
        public virtual DbSet<NAGRODY_KLIENCI> NAGRODY_KLIENCI { get; set; }
        public virtual DbSet<OSOBY> OSOBY { get; set; }
        public virtual DbSet<POJAZDY> POJAZDY { get; set; }
        public virtual DbSet<PRACOWNICY> PRACOWNICY { get; set; }
        public virtual DbSet<PRZEJAZDY> PRZEJAZDY { get; set; }
        public virtual DbSet<PRZYSTANKI> PRZYSTANKI { get; set; }
        public virtual DbSet<REZERWACJE> REZERWACJE { get; set; }
        public virtual DbSet<RODZAJE_BILETOW> RODZAJE_BILETOW { get; set; }
        public virtual DbSet<TANKOWANIA> TANKOWANIA { get; set; }
        public virtual DbSet<TRASA> TRASA { get; set; }
        public virtual DbSet<ULGI_REZERWACJA> ULGI_REZERWACJA { get; set; }
    }
}
