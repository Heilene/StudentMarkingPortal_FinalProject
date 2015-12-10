using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VisionCollegeDSEDMarkingPortal.DTO
{
    public class MarkingOverviewDTO
    {
        public int OverViewID { get; set; }
        public string OverviewTitle { get; set; }
        public string MyComment { get; set; }
        public int TitleID { get; set; }
        public bool CheckedValue { get; set; }
        public string MyAchieved { get; set; }

    }

    public class UniqueOverviewTitleDTO
    {
        public string myOverviewTitle { get; set; }
    }

  
}