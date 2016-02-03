using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VisionCollegeDSEDMarkingPortal.Models;
using VisionCollegeDSEDMarkingPortal.Models.ViewModels;
using VisionCollegeDSEDMarkingPortal.Repositories;
using VisionCollegeDSEDMarkingPortal.DTO;

namespace VisionCollegeDSEDMarkingPortal.Controllers
{
    public class StudentDetailsController : Controller
    {
        //Instantiate ViewModel and classes
        DatabaseCalls myDatabaseCalls = new DatabaseCalls();
        private StudentPortalDBEntities2 db = new StudentPortalDBEntities2();

        // GET: StudentDetails
        public async Task<ActionResult> Index()
        {
            return View(await db.StudentDetails.ToListAsync());
        }

        // GET: StudentDetails/Details/5
        public async Task<ActionResult> Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            StudentDetail studentDetail = await db.StudentDetails.FindAsync(id);


            if (studentDetail == null)
            {
                return HttpNotFound();
            }
            return View(studentDetail);
        }

        //GET:StudentDetail/MarkingToolView/5
        //how to pass values to controller http://stackoverflow.com/questions/8646944/persist-values-between-two-action-results-in-one-controller
        [HttpGet]
        public ActionResult MarkingToolView( int? id, int? studentID)
        {
            MarkingVM MyMarkingVM = new MarkingVM();
            ResultsDTO MyResults = new ResultsDTO();
           // StudentMarkingDTO MyStudentData = new StudentMarkingDTO();
            //return student deets
            MyResults = myDatabaseCalls.getStudentDetails(studentID, id);
            MyMarkingVM.StudentID = studentID;
            MyMarkingVM.ModuleID = id;
            
            //return marking overview
            MyMarkingVM.MyMarkingOverview = myDatabaseCalls.GetMarkingOverview(id);
            MyMarkingVM.MarkAchievedList = myDatabaseCalls.GetMarkingOverview(id);
            MyResults.ModuleID = id;

            //get unique titles of overview for display
            MyMarkingVM.MyOverviewTitles = myDatabaseCalls.GetUniqueTitle(id);

            //return marking rubric table value per row index
            MyMarkingVM.MyTableRow1 = myDatabaseCalls.GetRubricTable(1);
            MyMarkingVM.MyTableRow2 = myDatabaseCalls.GetRubricTable(2);
            MyMarkingVM.MyTableRow3 = myDatabaseCalls.GetRubricTable(3);
            MyMarkingVM.MyTableRow4 = myDatabaseCalls.GetRubricTable(4);
            MyMarkingVM.MyTableRow5 = myDatabaseCalls.GetRubricTable(5);


            //return Mark values for radio button
            MyMarkingVM.myMarksSet1 = myDatabaseCalls.GetMarkSetValues1();
            MyMarkingVM.myMarksSet2 = myDatabaseCalls.GetMarkSetValues2();
            MyMarkingVM.myMarksSet3 = myDatabaseCalls.GetMarkSetValues2();
            MyMarkingVM.myMarksSet4 = myDatabaseCalls.GetMarkSetValues3();
            MyMarkingVM.myMarksSet5 = myDatabaseCalls.GetMarkSetValues3();

            //Viewbags
            ViewBag.ModuleDeets = string.Format("{0} - {1}", MyResults.MCode, MyResults.MName);
            ViewBag.StudentDeets = string.Format("{0}", MyResults.SFullName);
            
           

            return View(MyMarkingVM);
        }

        //POST:StudentDetail/MarkingToolView/Create
        [HttpPost]
        public ActionResult MarkingToolView(MarkingVM MarkingModel)
        {
            if (ModelState.IsValid)
            {
                //Get Marking total value
                MarkingModel.MarkTotal = MarkingModel.GetTotalMark(MarkingModel.MyResults);

                //update results Database
                myDatabaseCalls.CreateNewResultsRecord(MarkingModel);

                return RedirectToAction("ResultView", new { id = MarkingModel.ModuleID, studentID = MarkingModel.StudentID});
            }

            return View(MarkingModel);
        }

        //GET: StudentDetails/ResultView/5
        public ActionResult ResultView(int? id, int? studentID)
        {
            ResultVM ResultViewModel = new ResultVM();
            ResultsDTO MyResultDetails = new ResultsDTO();

            
            //return student deets
            ResultViewModel.MyResults = myDatabaseCalls.GetStudentResults(studentID, id);
            ResultViewModel.StudentID = studentID;
            ResultViewModel.ModuleID = id;

            //Get mark total
            //ResultView.MarkTotal = ResultView.GetTotalMark(ResultView.MyResults);

            //return marking overview and Marks Achieved Values
            ResultViewModel.MyMarkingOverview = myDatabaseCalls.GetMarkingOverview(id);

            //get unique titles of overview for display one title per comment set
            ResultViewModel.MyOverviewTitles = myDatabaseCalls.GetUniqueTitle(id);

            //return marking rubric table value per row index
           ResultViewModel.MyTableRow1 = myDatabaseCalls.GetRubricTable(1);
           ResultViewModel.MyTableRow2 = myDatabaseCalls.GetRubricTable(2);
           ResultViewModel.MyTableRow3 = myDatabaseCalls.GetRubricTable(3);
           ResultViewModel.MyTableRow4 = myDatabaseCalls.GetRubricTable(4);
           ResultViewModel.MyTableRow5 = myDatabaseCalls.GetRubricTable(5);

            //get table cell background colours
            myDatabaseCalls.GetBackGroundValues(ResultViewModel.MyTableRow1, ResultViewModel.MyResults.RMark1);
            myDatabaseCalls.GetBackGroundValues(ResultViewModel.MyTableRow2, ResultViewModel.MyResults.RMark2);
            myDatabaseCalls.GetBackGroundValues(ResultViewModel.MyTableRow3, ResultViewModel.MyResults.RMark3);
            myDatabaseCalls.GetBackGroundValues(ResultViewModel.MyTableRow4, ResultViewModel.MyResults.RMark4);
            myDatabaseCalls.GetBackGroundValues(ResultViewModel.MyTableRow5, ResultViewModel.MyResults.RMark5);


            //Viewbags
            ViewBag.ModuleDeets = string.Format("{0} - {1}", ResultViewModel.MyResults.MCode, ResultViewModel.MyResults.MName);
            ViewBag.StudentDeets = string.Format("{0}", ResultViewModel.MyResults.SFullName);
            ViewBag.ModuleDescription = string.Format("{0}", ResultViewModel.MyResults.ModuleDescription);
            return View(ResultViewModel);
        }

        //GET:StudentDetail/MarkingToolView/5
        //how to pass values to controller http://stackoverflow.com/questions/8646944/persist-values-between-two-action-results-in-one-controller
        [HttpGet]
        public ActionResult EditMarkingView(int? id, int? studentID)
        {
            
            EditVM MyEditVM = new EditVM();
            MyEditVM.MyResults = myDatabaseCalls.GetStudentResults(studentID, id);
            MyEditVM.ResultID = MyEditVM.MyResults.ResultID;
            MyEditVM.ModuleID = MyEditVM.MyResults.ModuleID;


            //return marking overview
            MyEditVM.MyMarkingOverview = myDatabaseCalls.GetMarkingOverview(id);
            MyEditVM.MarkAchievedList = MyEditVM.MyResults.myAchievedList;
            MyEditVM.MyResults.ModuleID = id;

            //get checked value for AchievedElements
            MyEditVM.UpdateCheckedValues(MyEditVM.MarkAchievedList);
            
            //get unique titles of overview for display
            MyEditVM.MyOverviewTitles = myDatabaseCalls.GetUniqueTitle(id);

            //return marking rubric table value per row index
            MyEditVM.MyTableRow1 = myDatabaseCalls.GetRubricTable(1);
            MyEditVM.MyTableRow2 = myDatabaseCalls.GetRubricTable(2);
            MyEditVM.MyTableRow3 = myDatabaseCalls.GetRubricTable(3);
            MyEditVM.MyTableRow4 = myDatabaseCalls.GetRubricTable(4);
            MyEditVM.MyTableRow5 = myDatabaseCalls.GetRubricTable(5);


            //return Mark values for radio button
            MyEditVM.myMarksSet1 = myDatabaseCalls.GetMarkSetValues1();
            MyEditVM.myMarksSet2 = myDatabaseCalls.GetMarkSetValues2();
            MyEditVM.myMarksSet3 = myDatabaseCalls.GetMarkSetValues2();
            MyEditVM.myMarksSet4 = myDatabaseCalls.GetMarkSetValues3();
            MyEditVM.myMarksSet5 = myDatabaseCalls.GetMarkSetValues3();

            //Viewbags
            ViewBag.ModuleDeets = string.Format("{0} - {1}", MyEditVM.MyResults.MCode, MyEditVM.MyResults.MName);
            ViewBag.StudentDeets = string.Format("{0}", MyEditVM.MyResults.SFullName);


            return View(MyEditVM);
        }

        //POST:StudentDetail/MarkingToolView/Create
        [HttpPost]
        public ActionResult EditMarkingView(EditVM MyEditVM)
        {
            if (ModelState.IsValid)
            {

                //Get Marking total value to update new total value
               MyEditVM.MarkTotal = MyEditVM.GetTotalMark(MyEditVM.MyResults);

                //update results Database
               myDatabaseCalls.UpdateMarkingResults(MyEditVM);

                return RedirectToAction("ResultView", new { id = MyEditVM.ModuleID, studentID = MyEditVM.StudentID });
            }

            return View(MyEditVM);
        }

        // GET: StudentDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudentDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "StudentID,FirstName,MiddleName,LastName,Email,Phone,Picture,DateEnrolled")] StudentDetail studentDetail)
        {
            if (ModelState.IsValid)
            {
                db.StudentDetails.Add(studentDetail);
                await db.SaveChangesAsync();
                //Create Module set for each newly created student
                myDatabaseCalls.CreateModuleSetForeachStudent(studentDetail.StudentID);

                return RedirectToAction("Index");
            }

            return View(studentDetail);
        }

        // GET: StudentDetails/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentDetail studentDetail = await db.StudentDetails.FindAsync(id);
            if (studentDetail == null)
            {
                return HttpNotFound();
            }
            return View(studentDetail);
        }

        // POST: StudentDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "StudentID,FirstName,MiddleName,LastName,Email,Phone,Picture,DateEnrolled")] StudentDetail studentDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentDetail).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(studentDetail);
        }

        // GET: StudentDetails/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentDetail studentDetail = await db.StudentDetails.FindAsync(id);
            if (studentDetail == null)
            {
                return HttpNotFound();
            }
            return View(studentDetail);
        }

        // POST: StudentDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
           StudentDetail studentDetail = await db.StudentDetails.FindAsync(id);

            //Remove Enrollment details of student using student ID
            db.ModuleEnrollments.RemoveRange(db.ModuleEnrollments.Where(x => x.StudentFk == id));

            db.StudentDetails.Remove(studentDetail);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
