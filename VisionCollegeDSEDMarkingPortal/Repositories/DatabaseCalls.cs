using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VisionCollegeDSEDMarkingPortal.DTO;
using VisionCollegeDSEDMarkingPortal.Models;
using VisionCollegeDSEDMarkingPortal.Models.ViewModels;

namespace VisionCollegeDSEDMarkingPortal.Repositories
{
    public class DatabaseCalls
    {

        #region DATABASE CALLS FOR MARKINGTOOLVIEW

        //return student details for selected student
        public ResultsDTO getStudentDetails(int? MyStudentID, int? MyModuleID)
        {
            ResultsDTO myStudent = new ResultsDTO();
            using (var context = new StudentPortalDBEntities2())
            {
                var students =
                    from s in context.StudentDetails
                    join e in context.ModuleEnrollments on s.StudentID equals e.StudentFk
                    join m in context.ModuleDetails on e.ModuleFk equals m.ModuleID
                    where s.StudentID == MyStudentID && m.ModuleID == MyModuleID
                    select new
                    {
                        s.FirstName,
                        s.MiddleName,
                        s.LastName,
                        s.StudentID,
                        m.ModuleCode,
                        m.ModuleName,
                        m.ModuleID
                    };

                foreach (var student in students)
                {
                    //creates full name based on middlename
                    var fullName = FullNameGenerator(student.FirstName, student.MiddleName, student.LastName);

                    myStudent.SFullName = fullName;
                    myStudent.StudentID = student.StudentID;
                    myStudent.MCode = student.ModuleCode;
                    myStudent.MName = student.ModuleName;
                    myStudent.ModuleID = student.ModuleID;



                }
                return myStudent;
            }
        }



        /// <summary>
        /// Returns Marking Overview layout per module - 
        /// </summary>
        /// <param name="myModuleID"></param>
        /// <returns>OverviewID, OverviewTitle, Comments and Title ID</returns>
        public List<MarkingOverviewDTO> GetMarkingOverview(int? myModuleID)
        {
            List<MarkingOverviewDTO> MyMarkingOverview = new List<MarkingOverviewDTO>();
            using (var context = new StudentPortalDBEntities2())
            {
                var overview =
                     from mso in context.MarkingScheduleOverviews
                     join t in context.MarkingOverviewTitles on mso.OverviewTitleFK equals t.OverviewTitleID
                     join m in context.ModuleDetails on mso.ModuleFK equals m.ModuleID
                     where m.ModuleID == myModuleID
                     select new
                     {
                         t.TitleName,
                         mso.CommentItem,
                         mso.MSOverviewID,
                         t.OverviewTitleID
                     };

                foreach (var item in overview)
                {
                    MyMarkingOverview.Add(new MarkingOverviewDTO
                    {
                        OverViewID = item.MSOverviewID,
                        OverviewTitle = item.TitleName,
                        MyComment = item.CommentItem,
                        TitleID = item.OverviewTitleID,
                    });
                }
            }

            return MyMarkingOverview;
        }

        /// <summary>
        /// Get Unique titles to display comments for the Marking Overview table per module
        /// </summary>
        /// <param name="myModuleID"></param>
        /// <returns></returns>
        public List<UniqueOverviewTitleDTO> GetUniqueTitle(int? myModuleID)
        {
            List<UniqueOverviewTitleDTO> MyUniqueTitles = new List<UniqueOverviewTitleDTO>();
            using (var context = new StudentPortalDBEntities2())
            {
                var overview =
                   (from mso in context.MarkingScheduleOverviews
                    join t in context.MarkingOverviewTitles on mso.OverviewTitleFK equals t.OverviewTitleID
                    join m in context.ModuleDetails on mso.ModuleFK equals m.ModuleID
                    where m.ModuleID == myModuleID
                    select mso.MarkingOverviewTitle).Distinct();

                foreach (var item in overview)
                {
                    MyUniqueTitles.Add(new UniqueOverviewTitleDTO
                    {
                        myOverviewTitle = item.TitleName
                    });

                }

            }

            return MyUniqueTitles;
        }


        #endregion

        #region DATABASE CALLS FOR RESULTVIEW
        /// <summary>
        /// Return student marks, total and achieved list
        /// </summary>
        /// <param name="studentID"></param>
        /// <param name="moduleID"></param>
        /// <returns></returns>
        public ResultsDTO GetStudentResults(int? studentID, int? moduleID)
        {
            ResultsDTO MyStudentsResults = new ResultsDTO();
            using (var context = new StudentPortalDBEntities2())
            {
                //get result marks
                var myResults =
                     from e in context.ModuleEnrollments
                     join s in context.StudentDetails on e.StudentFk equals s.StudentID
                     join m in context.ModuleDetails on e.ModuleFk equals m.ModuleID
                     join r in context.Results on e.ReslutsFK equals r.ResultsID
                     where s.StudentID == studentID && m.ModuleID == moduleID
                     select new
                     {
                         s.FirstName,
                         s.MiddleName,
                         s.LastName,
                         r.ResultsID,
                         r.Mark1,
                         r.Mark2,
                         r.Mark3,
                         r.Mark4,
                         r.Mark5,
                         r.Summary,
                         r.Total,
                         m.ModuleCode,
                         m.ModuleName,
                         m.ModuleID,
                         m.ModuleDescription
                         

                     };

                foreach (var result in myResults)
                {
                    //creates full name based on middlename
                    var fullName = FullNameGenerator(result.FirstName, result.MiddleName, result.LastName);
                    MyStudentsResults.SFullName = fullName;
                    MyStudentsResults.RMark1 = result.Mark1;
                    MyStudentsResults.RMark2 = result.Mark2;
                    MyStudentsResults.RMark3 = result.Mark3;
                    MyStudentsResults.RMark4 = result.Mark4;
                    MyStudentsResults.RMark5 = result.Mark5;
                    MyStudentsResults.RTotal = result.Total;
                    MyStudentsResults.MCode = result.ModuleCode;
                    MyStudentsResults.MName = result.ModuleName;
                    MyStudentsResults.RSummary = result.Summary;
                    MyStudentsResults.ResultID = result.ResultsID;
                    MyStudentsResults.ModuleID = result.ModuleID;
                    MyStudentsResults.ModuleDescription = result.ModuleDescription;

                    //add achievement list per result ID
                    MyStudentsResults.myAchievedList = GetAchevedList(result.ResultsID);
                }
            }
            return MyStudentsResults;
        }
        /// <summary>
        /// Retrieve Achieved List using the Result/Student relationship from the Marking Overview and Titles
        /// </summary>
        /// <param name="MyResultID"></param>
        /// <returns></returns>
        public List<MarkingOverviewDTO> GetAchevedList(int MyResultID)
        {
            List<MarkingOverviewDTO> MyAchievedList = new List<MarkingOverviewDTO>();

            using (var context = new StudentPortalDBEntities2())
            {
                var achievedList =
                     from ae in context.AchievedElements
                     join r in context.Results on ae.ResultFk equals r.ResultsID
                     join mo in context.MarkingScheduleOverviews on ae.MSOverviewFK equals mo.MSOverviewID
                     join t in context.MarkingOverviewTitles on mo.OverviewTitleFK equals t.OverviewTitleID
                     where ae.ResultFk == MyResultID
                     select new
                     {
                         t.TitleName,
                         mo.CommentItem,
                         ae.AchievedItem,
                         ae.AchevedID
                     };
                foreach (var item in achievedList)
                {
                    MyAchievedList.Add(new MarkingOverviewDTO
                    {
                        OverviewTitle = item.TitleName,
                        MyComment = item.CommentItem,
                        MyAchieved = item.AchievedItem,
                        OverViewID = item.AchevedID
                    });
                }
            }

            return MyAchievedList;
        }

        #endregion

        #region DATABASE CALLS FOR RUBRIC TABLE AND MARK VALUES
        /// <summary>
        /// Returns table values for Rubric Marking table and Background values for each column
        /// </summary>
        /// <returns></returns>
        public List<RubricTableDTO> GetRubricTable(int rowIndex)
        {
            List<RubricTableDTO> myTable = new List<RubricTableDTO>();
            using (var context = new StudentPortalDBEntities2())
            {
                var table = context.MarkingScheduleTables.Where(t => t.MarkingScheduleID == rowIndex).Select(t => new
                {

                    t.MarkingScheduleID,
                    t.Title,
                    t.Excellent100,
                    t.Adequate80,
                    t.Poor60,
                    t.NotMet0,
                    t.Percentage,
                    t.ExcelentMark,
                    t.AdequateMark,
                    t.PoorMark,
                    t.NotMetMark

                });

                foreach (var item in table)
                {
                    myTable.Add(new RubricTableDTO
                    {
                        ID = item.MarkingScheduleID,
                        Title = item.Title,
                        ExcellentDesc = item.Excellent100,
                        AdequateDesc = item.Adequate80,
                        PoorDesc = item.Poor60,
                        NotMetDesc = item.NotMet0,
                        Percentage = item.Percentage,
                        ExcellentMark = item.ExcelentMark,
                        AdequateMark = item.AdequateMark,
                        PoorMark = item.PoorMark,
                        NotMetMark = item.NotMetMark
                    });
                }
            }
            return myTable;
        }

     /// <summary>
     /// Test values against table marks to change cell background colour according to value selected
     /// </summary>
     /// <param name="MyValues"></param>
     /// <param name="MyMark"></param>
        public void GetBackGroundValues(List<RubricTableDTO> MyValues, int MyMark)
        {
            foreach (var item in MyValues)
            {
                //test if mark values are equal to the rubric values, if it is then give the style property the string value to change the back ground colour
                if (MyMark == item.ExcellentMark)
                {
                    item.ExcellentBGColour = "background-color:lightblue";
                }
                if (MyMark == item.AdequateMark)
                {
                    item.AdequateBGColour = "background-color:lightblue";
                }
                if (MyMark == item.PoorMark)
                {
                    item.PoorBGColour = "background-color:lightblue";
                }
                if (MyMark == item.NotMetMark)
                {
                    item.NotMetBGColour = "background-color:lightblue";
                }
            }


          }


        /// <summary>
        /// Generate Marking Set for Rubric Table, returns list to use in RadioButton on MarkingToolView
        /// </summary>
        /// <returns></returns>
        public List<MarkDTO> GetMarkSetValues1()
        {
            return new[]
            {
                new MarkDTO { MarkValue = 50},
                new MarkDTO { MarkValue = 40},
                new MarkDTO { MarkValue = 30},
                new MarkDTO { MarkValue = 0 }

           }.ToList();

        }
        /// <summary>
        /// Generate Marking Set for Rubric Table, returns list to use in RadioButton on MarkingToolView
        /// </summary>
        /// <returns></returns>
        public List<MarkDTO> GetMarkSetValues2()
        {
            return new[]
            {
                new MarkDTO { MarkValue = 20},
                new MarkDTO { MarkValue = 16},
                new MarkDTO { MarkValue = 12},
                new MarkDTO { MarkValue = 0 }

           }.ToList();

        }
        /// <summary>
        /// Generate Marking Set for Rubric Table, returns list to use in RadioButton on MarkingToolView
        /// </summary>
        /// <returns></returns>
        public List<MarkDTO> GetMarkSetValues3()
        {
            return new[]
            {
                new MarkDTO { MarkValue = 5},
                new MarkDTO { MarkValue = 4},
                new MarkDTO { MarkValue = 3},
                new MarkDTO { MarkValue = 0 }

           }.ToList();

        }

        #endregion

        #region INSERT RECORDS DATACALLS
        /// <summary>
        /// Insert new result record with all marking overview values achieved. Input parameter is viewmodel values retrieved from MarkingToolView
        /// </summary>
        /// <param name="MyReturnValues"></param>
        public void CreateNewResultsRecord(MarkingVM MyReturnValues)
        {
            using (var context = new StudentPortalDBEntities2())
            {
                var myResult = new Result
                {
                    Mark1 = MyReturnValues.MyResults.RMark1,
                    Mark2 = MyReturnValues.MyResults.RMark2,
                    Mark3 = MyReturnValues.MyResults.RMark3,
                    Mark4 = MyReturnValues.MyResults.RMark4,
                    Mark5 = MyReturnValues.MyResults.RMark5,
                    Total = MyReturnValues.MarkTotal,
                    Summary = MyReturnValues.Summary

                };

                context.Results.Add(myResult);
                //Save results table first so last ID value can be retrieved
                context.SaveChanges();

                //retrieve the last result ID
                var resultID = context.Results.Select(r => r.ResultsID).Max();

                //get Enrollment ID using StudentID, ModuleID and last Result ID
                var myEnrollmentID = GetEnrollmentID(MyReturnValues.StudentID, MyReturnValues.ModuleID, resultID);

                //update my enrollement table
                var dataToUpdate =
                    from e in context.ModuleEnrollments
                    where e.ModulesAssignedID == myEnrollmentID
                    select e;

                var UpdateEnrollment = dataToUpdate.FirstOrDefault();

                if (UpdateEnrollment != null)
                {
                    UpdateEnrollment.ReslutsFK = resultID;
                }

                //if there are no achieved values return only rubric table values
                if (MyReturnValues.MarkAchievedList != null)
                {
                    //Add each achieved record asigned to result id
                    foreach (var item in MyReturnValues.MarkAchievedList)
                    {
                        var achieved = new AchievedElement
                        {
                            ResultFk = resultID,
                            MSOverviewFK = item.OverViewID,
                            AchievedItem = Convert.ToString(item.CheckedValue)
                        };

                        context.AchievedElements.Add(achieved);
                    }
                }
               
                context.SaveChanges();

            }
        }

        /// <summary>
        /// Generates a module set for the newly created student
        /// </summary>
        /// <param name="StudentID"></param>
        public void CreateModuleSetForeachStudent(int? StudentID)
        {
            List<int> MyModulesIDs = new List<int>();
            using (var context= new StudentPortalDBEntities2())
            {
                var modules =
                    from m in context.ModuleDetails
                    select m;

                //creates a list of ID's I can add to enrolment foreign key
                foreach (var item in modules)
                {
                    MyModulesIDs.Add(item.ModuleID);
                }

                foreach (var id in MyModulesIDs)
                {
                    var myEnrolment = new ModuleEnrollment
                    {
                        StudentFk = StudentID,
                        ModuleFk = id
                                              
                    };
                    context.ModuleEnrollments.Add(myEnrolment);
                    context.SaveChanges();

                }
            }
        }


        #endregion


        #region METHODS TO RETURN VALUES FOR DATABASE CALLS
        /// <summary>
        /// Find Enrollment ID for update table using StudentID, ModuleID and Results ID
        /// </summary>
        /// <param name="studentID"></param>
        /// <param name="moduleID"></param>
        /// <param name="resultID"></param>
        /// <returns></returns>
        private int GetEnrollmentID(int? studentID, int? moduleID, int resultID)
        {
            int myID = 0;
            using (var context = new StudentPortalDBEntities2())
            {
                var myEnrollment =
                    from e in context.ModuleEnrollments
                    where e.StudentFk == studentID && e.ModuleFk == moduleID
                    select new
                    {
                        e.ModulesAssignedID
                    };

                foreach (var ID in myEnrollment)
                {
                    myID = ID.ModulesAssignedID;
                }
            }
            return myID;
        }
        /// <summary>
        /// Return fullName depending on the excitance of middlename
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="middleName"></param>
        /// <param name="lastName"></param>
        /// <returns></returns>
        public string FullNameGenerator(string firstName, string middleName, string lastName)
        {
            if (middleName == null)
            {
                string fullName = string.Format("{0} {1}", firstName, lastName);
                return fullName;
            }
            else
            {
                string fullName = string.Format("{0} {1} {2}", firstName, middleName, lastName);
                return fullName;
            }

        }
        #endregion
        #region UPDATE DATABASE RECORD TABLE
        public void UpdateMarkingResults(EditVM myResults)
        {
            using (var context = new StudentPortalDBEntities2())
            {
                var resultDataCall =
                    from r in context.Results
                    where r.ResultsID == myResults.ResultID
                    select r;

                var UpdateResult = resultDataCall.FirstOrDefault();

                if (UpdateResult != null)
                {
                    UpdateResult.Summary = myResults.MyResults.RSummary;
                    UpdateResult.Mark1 = myResults.MyResults.RMark1;
                    UpdateResult.Mark2 = myResults.MyResults.RMark2;
                    UpdateResult.Mark3 = myResults.MyResults.RMark3;
                    UpdateResult.Mark4 = myResults.MyResults.RMark4;
                    UpdateResult.Mark5 = myResults.MyResults.RMark5;
                    UpdateResult.Total = myResults.MarkTotal;
                }

                context.SaveChanges();

                foreach (var item in myResults.MarkAchievedList)
                {
                    var achievedData =
                        from ae in context.AchievedElements
                        where ae.AchevedID == item.OverViewID
                        select ae;

                    var UpdateAchieved = achievedData.FirstOrDefault();

                    if (UpdateAchieved != null)
                    {
                        UpdateAchieved.AchievedItem = Convert.ToString(item.CheckedValue);
                    }

                    context.SaveChanges();
                }

            }

        }


        #endregion

    }



}