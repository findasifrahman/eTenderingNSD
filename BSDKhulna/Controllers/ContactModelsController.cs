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
    public class ContactModelsController : Controller
    {
        private AdminDBContext db = new AdminDBContext();

        // GET: ContactModels
        public ActionResult Index()
        {
            return View(db.ContactModels.ToList());
        }
        [AllowAnonymous]
        public ActionResult ViewContact()
        {
            ViewBag.welcome = db.welcomeNoticeModels.Select(x => x.WelcomeNotice).FirstOrDefault();
            return View(db.ContactModels.ToList());
        }
        // GET: ContactModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactModels contactModels = db.ContactModels.Find(id);
            if (contactModels == null)
            {
                return HttpNotFound();
            }
            return View(contactModels);
        }

        // GET: ContactModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ContactModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,duty,AddressLine1,AddressLine2,AddressLine3,AddressLine4,cell,phone,fax,email")] ContactModels contactModels)
        {
            if (ModelState.IsValid)
            {
                db.ContactModels.Add(contactModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contactModels);
        }

        // GET: ContactModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactModels contactModels = db.ContactModels.Find(id);
            if (contactModels == null)
            {
                return HttpNotFound();
            }
            return View(contactModels);
        }

        // POST: ContactModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,duty,AddressLine1,AddressLine2,AddressLine3,AddressLine4,cell,phone,fax,email")] ContactModels contactModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contactModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contactModels);
        }

        // GET: ContactModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactModels contactModels = db.ContactModels.Find(id);
            if (contactModels == null)
            {
                return HttpNotFound();
            }
            return View(contactModels);
        }

        // POST: ContactModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ContactModels contactModels = db.ContactModels.Find(id);
            db.ContactModels.Remove(contactModels);
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
