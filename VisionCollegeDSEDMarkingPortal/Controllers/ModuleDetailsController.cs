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

namespace VisionCollegeDSEDMarkingPortal.Controllers
{
    public class ModuleDetailsController : Controller
    {
        private StudentPortalDBEntities2 db = new StudentPortalDBEntities2();

        // GET: ModuleDetails
        public async Task<ActionResult> Index()
        {
            return View(await db.ModuleDetails.ToListAsync());
        }

        // GET: ModuleDetails/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModuleDetail moduleDetail = await db.ModuleDetails.FindAsync(id);
            if (moduleDetail == null)
            {
                return HttpNotFound();
            }
            return View(moduleDetail);
        }

        // GET: ModuleDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ModuleDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ModuleID,ModuleCode,ModuleName,ModuleDescription,AssignmentFile")] ModuleDetail moduleDetail)
        {
            if (ModelState.IsValid)
            {
                db.ModuleDetails.Add(moduleDetail);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(moduleDetail);
        }

        // GET: ModuleDetails/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModuleDetail moduleDetail = await db.ModuleDetails.FindAsync(id);
            if (moduleDetail == null)
            {
                return HttpNotFound();
            }
            return View(moduleDetail);
        }

        // POST: ModuleDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ModuleID,ModuleCode,ModuleName,ModuleDescription,AssignmentFile")] ModuleDetail moduleDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(moduleDetail).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(moduleDetail);
        }

        // GET: ModuleDetails/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModuleDetail moduleDetail = await db.ModuleDetails.FindAsync(id);

            if (moduleDetail == null)
            {
                return HttpNotFound();
            }
            return View(moduleDetail);
        }

        // POST: ModuleDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ModuleDetail moduleDetail = await db.ModuleDetails.FindAsync(id);
            db.ModuleDetails.Remove(moduleDetail);
            //Remove Marking Overview details of module using module ID
            db.MarkingScheduleOverviews.RemoveRange(db.MarkingScheduleOverviews.Where(x => x.ModuleFK == id));
            //Remove Enrollment details of student using student ID
            db.ModuleEnrollments.RemoveRange(db.ModuleEnrollments.Where(x => x.ModuleFk == id));

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
