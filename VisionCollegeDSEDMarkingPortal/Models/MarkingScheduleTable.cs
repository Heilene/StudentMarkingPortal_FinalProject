//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VisionCollegeDSEDMarkingPortal.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class MarkingScheduleTable
    {
        public int MarkingScheduleID { get; set; }
        public string Title { get; set; }
        public string Excellent100 { get; set; }
        public int ExcelentMark { get; set; }
        public string Adequate80 { get; set; }
        public int AdequateMark { get; set; }
        public string Poor60 { get; set; }
        public int PoorMark { get; set; }
        public string NotMet0 { get; set; }
        public int NotMetMark { get; set; }
        public Nullable<int> Percentage { get; set; }
    }
}
