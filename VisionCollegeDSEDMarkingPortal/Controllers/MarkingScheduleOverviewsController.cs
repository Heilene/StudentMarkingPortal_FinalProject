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
    public class MarkingScheduleOverviewsController : Controller
    {
        private StudentPortalDBEntities2 db = new StudentPortalDBEntities2();

        // GET: MarkingScheduleOverviews
        public ActionResult Index()
        {
            var markingScheduleOverviews = db.MarkingScheduleOverviews.Include(m => m.MarkingOverviewTitle).Include(m => m.ModuleDetail);
            return View(markingScheduleOverviews.ToList());
        }

        // GET: MarkingScheduleOverviews/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MarkingScheduleOverview markingScheduleOverview = db.MarkingScheduleOverviews.Find(id);
            if (markingScheduleOverview == null)
            {
                return HttpNotFound();
            }
            return View(markingScheduleOverview);
        }

        // GET: MarkingScheduleOverviews/Create
        public ActionResult Create()
        {
            ViewBag.OverviewTitleFK = new SelectList(db.MarkingOverviewTitles, "OverviewTitleID", "TitleName");
            ViewBag.ModuleFK = new SelectList(db.ModuleDetails, "ModuleID", "ModuleCode");
            return View();
        }

        // POST: MarkingScheduleOverviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MSOverviewID,ModuleFK,OverviewTitleFK,CommentItem")] MarkingScheduleOverview markingScheduleOverview)
        {
            if (ModelState.IsValid)
            {
                db.MarkingScheduleOverviews.Add(markingScheduleOverview);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OverviewTitleFK = new SelectList(db.MarkingOverviewTitles, "OverviewTitleID", "TitleName", markingScheduleOverview.OverviewTitleFK);
            ViewBag.ModuleFK = new SelectList(db.ModuleDetails, "ModuleID", "ModuleCode", markingScheduleOverview.ModuleFK);
            return View(markingScheduleOverview);
        }

        // GET: MarkingScheduleOverviews/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MarkingScheduleOverview markingScheduleOverview = db.MarkingScheduleOverviews.Find(id);
            if (markingScheduleOverview == null)
            {
                return HttpNotFound();
            }
            ViewBag.OverviewTitleFK = new SelectList(db.MarkingOverviewTitles, "OverviewTitleID", "TitleName", markingScheduleOverview.OverviewTitleFK);
            ViewBag.ModuleFK = new SelectList(db.ModuleDetails, "ModuleID", "ModuleCode", markingScheduleOverview.ModuleFK);
            return View(markingScheduleOverview);
        }

        // POST: MarkingScheduleOverviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MSOverviewID,ModuleFK,OverviewTitleFK,CommentItem")] MarkingScheduleOverview markingScheduleOverview)
        {
            if (ModelState.IsValid)
            {
                db.Entry(markingScheduleOverview).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OverviewTitleFK = new SelectList(db.MarkingOverviewTitles, "OverviewTitleID", "TitleName", markingScheduleOverview.OverviewTitleFK);
            ViewBag.ModuleFK = new SelectList(db.ModuleDetails, "ModuleID", "ModuleCode", markingScheduleOverview.ModuleFK);
            return View(markingScheduleOverview);
        }

        // GET: MarkingScheduleOverviews/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MarkingScheduleOverview markingScheduleOverview = db.MarkingScheduleOverviews.Find(id);
            if (markingScheduleOverview == null)
            {
                return HttpNotFound();
            }
            return View(markingScheduleOverview);
        }

        // POST: MarkingScheduleOverviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MarkingScheduleOverview markingScheduleOverview = db.MarkingScheduleOverviews.Find(id);
            db.MarkingScheduleOverviews.Remove(markingScheduleOverview);
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
