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
    
    public partial class ClearingAccount
    {
        public int id { get; set; }
        public Nullable<int> memberid { get; set; }
        public string account { get; set; }
        public Nullable<short> type { get; set; }
        public Nullable<int> currency { get; set; }
        public Nullable<decimal> blnc { get; set; }
        public Nullable<decimal> sblnc { get; set; }
        public Nullable<long> linkaccount { get; set; }
        public Nullable<short> state { get; set; }
        public Nullable<System.DateTime> modified { get; set; }
    }
}
