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
    
    public partial class ActiveSessions
    {
        public int id { get; set; }
        public int sessionid { get; set; }
        public string isactive { get; set; }
        public Nullable<System.TimeSpan> starttime { get; set; }
        public Nullable<System.TimeSpan> endtime { get; set; }
        public Nullable<System.TimeSpan> tduration { get; set; }
        public Nullable<int> matched { get; set; }
        public string state { get; set; }
    }
}