//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Admin.dbBind
{
    using System;
    using System.Collections.Generic;
    
    public partial class Session
    {
        public long id { get; set; }
        public Nullable<long> boardid { get; set; }
        public string name { get; set; }
        public Nullable<System.TimeSpan> stime { get; set; }
        public Nullable<int> duration { get; set; }
        public Nullable<short> algorithm { get; set; }
        public Nullable<int> match { get; set; }
        public Nullable<short> allowedtypes { get; set; }
        public string description { get; set; }
        public Nullable<short> state { get; set; }
        public Nullable<System.DateTime> modified { get; set; }
        public Nullable<bool> isactive { get; set; }
        public Nullable<System.DateTime> starttime { get; set; }
        public Nullable<System.DateTime> endtime { get; set; }
        public string tduration { get; set; }
        public Nullable<long> matched { get; set; }
        public Nullable<bool> editorder { get; set; }
        public Nullable<bool> delorder { get; set; }
        public Nullable<short> markettype { get; set; }
    }
}
