using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VisionCollegeDSEDMarkingPortal.Models.ViewModels;

namespace VisionCollegeDSEDMarkingPortal.DTO
{
    public class ResultsDTO
    {  //Naming conventions - first letter of each property represents the table/class where it comes from
        public string SFullName { get; set; }
        public string MCode { get; set; }
        public string MName { get; set; }
        [DisplayName("Summary")]
        public string RSummary { get; set; }
        public int RMark1 { get; set; }
        public int RMark2 { get; set; }
        public int RMark3 { get; set; }
        public int RMark4 { get; set; }
        public int RMark5{ get; set; }
        public int? RTotal { get; set; }
        public DateTime? AAssignmentSubmittedOn { get; set; }
        public int? StudentID { get; set; }
        public int? ModuleID { get; set; }
        public List<MarkingOverviewDTO> myAchievedList { get; set; }
        public int EnrollmentID { get; set; }
        public int? ResultID { get; set; }
        public string ModuleDescription { get; set; }


    }
}