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
    
    public partial class Session
    {
        public int id { get; set; }
        public int boardid { get; set; }
        public string name { get; set; }
        public Nullable<System.DateTime> stime { get; set; }
        public Nullable<System.TimeSpan> duration { get; set; }
        public Nullable<short> algorithm { get; set; }
        public Nullable<short> match { get; set; }
        public string allowedtypes { get; set; }
        public string description { get; set; }
        public Nullable<short> state { get; set; }
        public Nullable<System.DateTime> modified { get; set; }
        public string isactive { get; set; }
        public Nullable<System.DateTime> starttime { get; set; }
        public Nullable<System.DateTime> endtime { get; set; }
        public string tduration { get; set; }
        public Nullable<long> matched { get; set; }
        public string editorder { get; set; }
        public string delorder { get; set; }
        public string markettype { get; set; }
    
        public virtual Session Session1 { get; set; }
        public virtual Session Session2 { get; set; }
        public virtual Session Session11 { get; set; }
        public virtual Session Session3 { get; set; }
    }
}
