using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VisionCollegeDSEDMarkingPortal.DTO;

namespace VisionCollegeDSEDMarkingPortal.Models.ViewModels
{
    public class MarkingVM
    {
        public IEnumerable<MarkingOverviewDTO> MyMarkingOverview { get; set; }
        //public RubricTableDTO MyTableHeadings { get; set; }
        public ResultsDTO MyResults { get; set; }
        public List<MarkDTO> myMarksSet1 { get; set; }              //Loads the mark values for each set for the radiobuttons
        public List<MarkDTO> myMarksSet2 { get; set; }              //Loads the mark values for each set for the radiobuttons
        public List<MarkDTO> myMarksSet3 { get; set; }              //Loads the mark values for each set for the radiobuttons
        public List<MarkDTO> myMarksSet4 { get; set; }              //Loads the mark values for each set for the radiobuttons
        public List<MarkDTO> myMarksSet5 { get; set; }              //Loads the mark values for each set for the radiobuttons
        public List<UniqueOverviewTitleDTO> MyOverviewTitles { get; set; }
        public List<RubricTableDTO> MyTableRow1 { get; set; }        //Loads rubricTable rows
        public List<RubricTableDTO> MyTableRow2 { get; set; }        //Loads rubricTable rows
        public List<RubricTableDTO> MyTableRow3 { get; set; }        //Loads rubricTable rows
        public List<RubricTableDTO> MyTableRow4 { get; set; }        //Loads rubricTable rows
        public List<RubricTableDTO> MyTableRow5 { get; set; }        //Loads rubricTable rows
        public int? StudentID { get; set; }
        public int? ModuleID { get; set; }
        public int? ResultID { get; set; }
        public string Summary { get; set; }
        public int? MarkTotal { get; set; }
        public List<MarkingOverviewDTO> MarkAchievedList { get; set; }

        //constructor for default values to handle required fields
        public MarkingVM()
        {
        }

        /// <summary>
        /// Calculate total marks from rubric table
        /// </summary>
        /// <param name="myMarks"></param>
        public int? GetTotalMark(ResultsDTO MyResults)
        {
            return MarkTotal = MyResults.RMark1 + MyResults.RMark2 + MyResults.RMark3 + MyResults.RMark4 + MyResults.RMark5;
         }

        /// <summary>
        /// Get checked values from Achieved
        /// </summary>
        /// <param name="MyAchievedValue"></param>
        public void UpdateCheckedValues(List<MarkingOverviewDTO> MyAchievedValue)
        {
            foreach (var item in MyAchievedValue)
            {
                if (item.MyAchieved == "True")
                {
                    item.CheckedValue = true;
                }
                else
                {
                    item.CheckedValue = false;
                }
            }

          
        }






        /// <summary>
        /// Method to return the assignment id using student id and module id
        /// </summary>
        /// <param name="StudentID"></param>
        /// <param name="ModuleID"></param>
        /// <returns></returns>
        //private int GetAssignmentID(int? StudentID, int? ModuleID)
        //{

        //    using (var context = new StudentPortalDBEntities2())
        //    {
        //       var assignmentID =
        //        //   from a in context.AssignmentSubmissions
        //        //   join s in context.StudentDetails on a.StudentFk equals s.StudentID
        //        //   join m in context.ModuleDetails on a.ModuleFk equals m.ModuleID
        //        //   where s.StudentID == StudentID || m.ModuleID == ModuleID
        //        //   select a.AssignmentSubmittedID;

        //        //int myAssignmentId = Convert.ToInt16(assignmentID);

        //        return (myAssignmentId);
        //    }

        // }
    }
}
