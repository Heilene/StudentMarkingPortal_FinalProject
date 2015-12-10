using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VisionCollegeDSEDMarkingPortal.DTO
{
    public class MarkDTO
    {
        //public int Excellent100 { get; set; }
        //public int Adequate80 { get; set; }
        //public int Poor60 { get; set; }
        //public int NotMet0 { get; set; }
       
        public int? MarkValue { get; set; }
        public string MarkValueDescription { get; set; }


    }
}