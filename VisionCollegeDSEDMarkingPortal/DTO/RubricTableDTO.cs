using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VisionCollegeDSEDMarkingPortal.DTO
{
    public class RubricTableDTO
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string ExcellentDesc { get; set; }
        public string AdequateDesc { get; set; }
        public string PoorDesc { get; set; }
        public string NotMetDesc { get; set; }
        public int? Percentage { get; set; }
        public int ExcellentMark { get; set; }
        public int AdequateMark { get; set; }
        public int PoorMark { get; set; }
        public int NotMetMark { get; set; }
        public string ExcellentBGColour { get; set; }
        public string AdequateBGColour { get; set; }
        public string PoorBGColour { get; set; }
        public string NotMetBGColour { get; set; }

        public RubricTableDTO()
        {
          //  ExcellentBGColour = "background-color:lightblue";
        }


    }
}