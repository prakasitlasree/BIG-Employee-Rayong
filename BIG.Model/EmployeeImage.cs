//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BIG.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class EmployeeImage
    {
        public int ID { get; set; }
        public string EMP_ID { get; set; }
        public byte[] PHOTO { get; set; }
        public string TYPE { get; set; }
        public Nullable<System.DateTime> CREATE_DATE { get; set; }
    }
}