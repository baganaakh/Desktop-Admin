//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class MarketMakers
    {
        public int id { get; set; }
        public Nullable<int> contactid { get; set; }
        public Nullable<int> memberid { get; set; }
        public Nullable<long> accountid { get; set; }
        public Nullable<System.DateTime> startdate { get; set; }
        public Nullable<System.DateTime> enddate { get; set; }
        public Nullable<int> ticks { get; set; }
        public string description { get; set; }
        public Nullable<int> orderlimit { get; set; }
        public Nullable<short> state { get; set; }
        public Nullable<System.DateTime> modified { get; set; }
    }
}
