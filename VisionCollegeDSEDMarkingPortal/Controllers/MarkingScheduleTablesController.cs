using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VisionCollegeDSEDMarkingPortal.Models;

namespace VisionCollegeDSEDMarkingPortal.Controllers
{
    public class MarkingScheduleTablesController : Controller
    {
        private StudentPortalDBEntities2 db = new StudentPortalDBEntities2();

        // GET: MarkingScheduleTables
        public ActionResult Index()
        {
            return View(db.MarkingScheduleTables.ToList());
        }

        // GET: MarkingScheduleTables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MarkingScheduleTable markingScheduleTable = db.MarkingScheduleTables.Find(id);
            if (markingScheduleTable == null)
            {
                return HttpNotFound();
            }
            return View(markingScheduleTable);
        }

        // GET: MarkingScheduleTables/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MarkingScheduleTables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MarkingScheduleID,Title,Excellent100,Adequate80,Poor60,NotMet0,Percentage")] MarkingScheduleTable markingScheduleTable)
        {
            if (ModelState.IsValid)
            {
                db.MarkingScheduleTables.Add(markingScheduleTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(markingScheduleTable);
        }

        // GET: MarkingScheduleTables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MarkingScheduleTable markingScheduleTable = db.MarkingScheduleTables.Find(id);
            if (markingScheduleTable == null)
            {
                return HttpNotFound();
            }
            return View(markingScheduleTable);
        }

        // POST: MarkingScheduleTables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MarkingScheduleID,Title,Excellent100,Adequate80,Poor60,NotMet0,Percentage")] MarkingScheduleTable markingScheduleTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(markingScheduleTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(markingScheduleTable);
        }

        // GET: MarkingScheduleTables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MarkingScheduleTable markingScheduleTable = db.MarkingScheduleTables.Find(id);
            if (markingScheduleTable == null)
            {
                return HttpNotFound();
            }
            return View(markingScheduleTable);
        }

        // POST: MarkingScheduleTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MarkingScheduleTable markingScheduleTable = db.MarkingScheduleTables.Find(id);
            db.MarkingScheduleTables.Remove(markingScheduleTable);
            db.SaveChanges();
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
