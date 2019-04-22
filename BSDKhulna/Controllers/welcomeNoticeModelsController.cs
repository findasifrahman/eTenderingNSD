using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BSDKhulna.Models;

namespace BSDKhulna.Controllers
{
    public class welcomeNoticeModelsController : Controller
    {
        private AdminDBContext db = new AdminDBContext();

        // GET: welcomeNoticeModels
        [Authorize(Roles = "Admin,IT")]
        public ActionResult Index()
        {
            return View(db.welcomeNoticeModels.ToList());
        }

        // GET: welcomeNoticeModels/Details/5
        [Authorize(Roles = "Admin,IT")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            welcomeNoticeModels welcomeNoticeModels = db.welcomeNoticeModels.Find(id);
            if (welcomeNoticeModels == null)
            {
                return HttpNotFound();
            }
            return View(welcomeNoticeModels);
        }

        // GET: welcomeNoticeModels/Create
        [Authorize(Roles = "Admin,IT")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: welcomeNoticeModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin,IT")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,WelcomeNotice")] welcomeNoticeModels welcomeNoticeModels)
        {
            if (ModelState.IsValid)
            {
                db.welcomeNoticeModels.Add(welcomeNoticeModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(welcomeNoticeModels);
        }

        // GET: welcomeNoticeModels/Edit/5
        [Authorize(Roles = "Admin,IT")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            welcomeNoticeModels welcomeNoticeModels = db.welcomeNoticeModels.Find(id);
            if (welcomeNoticeModels == null)
            {
                return HttpNotFound();
            }
            return View(welcomeNoticeModels);
        }

        // POST: welcomeNoticeModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin,IT")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,WelcomeNotice")] welcomeNoticeModels welcomeNoticeModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(welcomeNoticeModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(welcomeNoticeModels);
        }

        // GET: welcomeNoticeModels/Delete/5
        [Authorize(Roles = "Admin,IT")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            welcomeNoticeModels welcomeNoticeModels = db.welcomeNoticeModels.Find(id);
            if (welcomeNoticeModels == null)
            {
                return HttpNotFound();
            }
            return View(welcomeNoticeModels);
        }

        // POST: welcomeNoticeModels/Delete/5
        [Authorize(Roles = "Admin,IT")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            welcomeNoticeModels welcomeNoticeModels = db.welcomeNoticeModels.Find(id);
            db.welcomeNoticeModels.Remove(welcomeNoticeModels);
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
