﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace pages.dbBind
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class demoEntities10 : DbContext
    {
        public demoEntities10()
            : base("name=demoEntities10")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<ActiveSessions> ActiveSessions { get; set; }
        public virtual DbSet<Assets> Assets { get; set; }
        public virtual DbSet<BoardInstruments> BoardInstruments { get; set; }
        public virtual DbSet<Boards> Boards { get; set; }
        public virtual DbSet<Calendar> Calendar { get; set; }
        public virtual DbSet<ClearingAccounts> ClearingAccounts { get; set; }
        public virtual DbSet<Contracts> Contracts { get; set; }
        public virtual DbSet<dayType> dayType { get; set; }
        public virtual DbSet<Deal2> Deal2 { get; set; }
        public virtual DbSet<DealerAccounts> DealerAccounts { get; set; }
        public virtual DbSet<GroupUsers> GroupUsers { get; set; }
        public virtual DbSet<Invoices> Invoices { get; set; }
        public virtual DbSet<Margins> Margins { get; set; }
        public virtual DbSet<MarketMakers> MarketMakers { get; set; }
        public virtual DbSet<Members> Members { get; set; }
        public virtual DbSet<mtype> mtype { get; set; }
        public virtual DbSet<Participants> Participants { get; set; }
        public virtual DbSet<RefPrice> RefPrice { get; set; }
        public virtual DbSet<Rights> Rights { get; set; }
        public virtual DbSet<Securities> Securities { get; set; }
        public virtual DbSet<Settings> Settings { get; set; }
        public virtual DbSet<side> side { get; set; }
        public virtual DbSet<Spreads> Spreads { get; set; }
        public virtual DbSet<States> States { get; set; }
        public virtual DbSet<stype> stype { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Table> Table { get; set; }
        public virtual DbSet<TickSizeTable> TickSizeTable { get; set; }
        public virtual DbSet<Trans> Trans { get; set; }
        public virtual DbSet<transType> transType { get; set; }
        public virtual DbSet<UserAccounts> UserAccounts { get; set; }
        public virtual DbSet<UserGroups> UserGroups { get; set; }
        public virtual DbSet<users> users { get; set; }
        public virtual DbSet<Deals> Deals { get; set; }
        public virtual DbSet<Dealtype> Dealtype { get; set; }
        public virtual DbSet<Fee> Fee { get; set; }
        public virtual DbSet<GroupRights> GroupRights { get; set; }
        public virtual DbSet<InvoiceDetails> InvoiceDetails { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Session> Session { get; set; }
        public virtual DbSet<Softwares> Softwares { get; set; }
    }
}
