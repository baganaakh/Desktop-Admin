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
    
    public partial class Order
    {
        public long id { get; set; }
        public Nullable<short> side { get; set; }
        public Nullable<long> memberid { get; set; }
        public Nullable<long> accountid { get; set; }
        public Nullable<long> assetid { get; set; }
        public Nullable<int> qty { get; set; }
        public Nullable<decimal> price { get; set; }
        public Nullable<short> state { get; set; }
        public System.DateTime modified { get; set; }
        public Nullable<short> dealType { get; set; }
        public string connect { get; set; }
        public Nullable<int> day { get; set; }
        public Nullable<decimal> totSum { get; set; }
        public Nullable<decimal> toPay { get; set; }
        public Nullable<decimal> interests { get; set; }
        public Nullable<decimal> fee { get; set; }
        public Nullable<long> boardId { get; set; }
    }
}
