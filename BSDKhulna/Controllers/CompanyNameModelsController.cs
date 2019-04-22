using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BSDKhulna.Models;
using BSDKhulna.fonts;

namespace BSDKhulna.Controllers
{
    public class CompanyNameModelsController : Controller
    {
        private AdminDBContext db = new AdminDBContext();

        // GET: CompanyNameModels
        [Authorize(Roles = "Admin,IT,PPC")]
        public ActionResult Index()
        {
            return View(db.CompanyNameModels.ToList());
        }
        [Authorize(Roles = "Admin,IT,PPC")]
        // GET: CompanyNameModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyNameModels companyNameModels = db.CompanyNameModels.Find(id);
            if (companyNameModels == null)
            {
                return HttpNotFound();
            }
            return View(companyNameModels);
        }
        [Authorize(Roles = "Admin,IT,PPC")]
        public ActionResult PartialCreate()
        {
            return View("Create");
        }
        [Authorize(Roles = "Admin,IT,PPC")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PartialCreate([Bind(Include = "ID,CompanyName,OwnerName,Address")] CompanyNameModels companyNameModels)
        {
            if (ModelState.IsValid)
            {
                db.CompanyNameModels.Add(companyNameModels);
                db.SaveChanges();
                return View("Create","SupplierModels",db.SupplierModels);//RedirectToAction("Index");
            }

            return View(companyNameModels);
        }
        [Authorize(Roles = "Admin,IT,PPC")]
        // GET: CompanyNameModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CompanyNameModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin,IT,PPC")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CompanyName,OwnerName,Address")] CompanyNameModels companyNameModels)
        {
            if (ModelState.IsValid)
            {
                db.CompanyNameModels.Add(companyNameModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(companyNameModels);
        }
        [Authorize(Roles = "Admin,IT,PPC")]
        // GET: CompanyNameModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyNameModels companyNameModels = db.CompanyNameModels.Find(id);
            if (companyNameModels == null)
            {
                return HttpNotFound();
            }
            return View(companyNameModels);
        }

        // POST: CompanyNameModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin,IT,PPC")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CompanyName,OwnerName,Address")] CompanyNameModels companyNameModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(companyNameModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(companyNameModels);
        }
        [Authorize(Roles = "Admin,IT,PPC")]
        // GET: CompanyNameModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyNameModels companyNameModels = db.CompanyNameModels.Find(id);
            if (companyNameModels == null)
            {
                return HttpNotFound();
            }
            return View(companyNameModels);
        }

        // POST: CompanyNameModels/Delete/5
        [Authorize(Roles = "Admin,IT,PPC")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CompanyNameModels companyNameModels = db.CompanyNameModels.Find(id);
            db.CompanyNameModels.Remove(companyNameModels);
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
