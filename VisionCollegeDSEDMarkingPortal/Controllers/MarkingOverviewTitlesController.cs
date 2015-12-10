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
    public class MarkingOverviewTitlesController : Controller
    {
        private StudentPortalDBEntities2 db = new StudentPortalDBEntities2();

        // GET: MarkingOverviewTitles
        public ActionResult Index()
        {
            return View(db.MarkingOverviewTitles.ToList());
        }

        // GET: MarkingOverviewTitles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MarkingOverviewTitle markingOverviewTitle = db.MarkingOverviewTitles.Find(id);
            if (markingOverviewTitle == null)
            {
                return HttpNotFound();
            }
            return View(markingOverviewTitle);
        }

        // GET: MarkingOverviewTitles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MarkingOverviewTitles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OverviewTitleID,TitleName")] MarkingOverviewTitle markingOverviewTitle)
        {
            if (ModelState.IsValid)
            {
                db.MarkingOverviewTitles.Add(markingOverviewTitle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(markingOverviewTitle);
        }

        // GET: MarkingOverviewTitles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MarkingOverviewTitle markingOverviewTitle = db.MarkingOverviewTitles.Find(id);
            if (markingOverviewTitle == null)
            {
                return HttpNotFound();
            }
            return View(markingOverviewTitle);
        }

        // POST: MarkingOverviewTitles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OverviewTitleID,TitleName")] MarkingOverviewTitle markingOverviewTitle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(markingOverviewTitle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(markingOverviewTitle);
        }

        // GET: MarkingOverviewTitles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MarkingOverviewTitle markingOverviewTitle = db.MarkingOverviewTitles.Find(id);
            if (markingOverviewTitle == null)
            {
                return HttpNotFound();
            }
            return View(markingOverviewTitle);
        }

        // POST: MarkingOverviewTitles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MarkingOverviewTitle markingOverviewTitle = db.MarkingOverviewTitles.Find(id);
            db.MarkingOverviewTitles.Remove(markingOverviewTitle);
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
