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
    public class ModuleEnrollmentsController : Controller
    {
        private StudentPortalDBEntities2 db = new StudentPortalDBEntities2();

        // GET: ModuleEnrollments
        public async Task<ActionResult> Index()
        {
            var moduleEnrollments = db.ModuleEnrollments.Include(m => m.ModuleDetail).Include(m => m.StudentDetail);
            return View(await moduleEnrollments.ToListAsync());
        }

        // GET: ModuleEnrollments/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModuleEnrollment moduleEnrollment = await db.ModuleEnrollments.FindAsync(id);
            if (moduleEnrollment == null)
            {
                return HttpNotFound();
            }
            

            return View(moduleEnrollment);
        }

        // GET: ModuleEnrollments/Create
        public ActionResult Create()
        {
            ViewBag.ModuleFk = new SelectList(db.ModuleDetails, "ModuleID", "ModuleCode");
            ViewBag.StudentFk = new SelectList(db.StudentDetails, "StudentID", "FirstName");
            return View();
        }

        // POST: ModuleEnrollments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ModulesAssignedID,StudentFk,ModuleFk")] ModuleEnrollment moduleEnrollment)
        {
            if (ModelState.IsValid)
            {
                db.ModuleEnrollments.Add(moduleEnrollment);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ModuleFk = new SelectList(db.ModuleDetails, "ModuleID", "ModuleCode", moduleEnrollment.ModuleFk);
            ViewBag.StudentFk = new SelectList(db.StudentDetails, "StudentID", "FirstName", moduleEnrollment.StudentFk);
            return View(moduleEnrollment);
        }

        // GET: ModuleEnrollments/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModuleEnrollment moduleEnrollment = await db.ModuleEnrollments.FindAsync(id);
            if (moduleEnrollment == null)
            {
                return HttpNotFound();
            }
            ViewBag.ModuleFk = new SelectList(db.ModuleDetails, "ModuleID", "ModuleCode", moduleEnrollment.ModuleFk);
            ViewBag.StudentFk = new SelectList(db.StudentDetails, "StudentID", "FirstName", moduleEnrollment.StudentFk);
            return View(moduleEnrollment);
        }

        // POST: ModuleEnrollments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ModulesAssignedID,StudentFk,ModuleFk")] ModuleEnrollment moduleEnrollment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(moduleEnrollment).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ModuleFk = new SelectList(db.ModuleDetails, "ModuleID", "ModuleCode", moduleEnrollment.ModuleFk);
            ViewBag.StudentFk = new SelectList(db.StudentDetails, "StudentID", "FirstName", moduleEnrollment.StudentFk);
            return View(moduleEnrollment);
        }

        // GET: ModuleEnrollments/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ModuleEnrollment moduleEnrollment = await db.ModuleEnrollments.FindAsync(id);
            if (moduleEnrollment == null)
            {
                return HttpNotFound();
            }
            return View(moduleEnrollment);
        }

        // POST: ModuleEnrollments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ModuleEnrollment moduleEnrollment = await db.ModuleEnrollments.FindAsync(id);
            db.ModuleEnrollments.Remove(moduleEnrollment);
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
