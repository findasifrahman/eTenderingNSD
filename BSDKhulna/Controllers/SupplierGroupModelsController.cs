using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BSDKhulna.Models;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;

namespace BSDKhulna.Controllers
{
    public class SupplierGroupModelsController : Controller
    {
        private AdminDBContext db = new AdminDBContext();

        // GET: SupplierGroupModels
        [Authorize(Roles = "Admin,IT,PPC")]
        public ActionResult Index()
        {
            return View(db.SupplierGroupModels.ToList());
        }

        // GET: SupplierGroupModels/Details/5
        [Authorize(Roles = "Admin,IT,PPC")]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupplierGroupModels supplierGroupModels = db.SupplierGroupModels.Where(x => x.GroupName == id).FirstOrDefault();
            if (supplierGroupModels == null)
            {
                return HttpNotFound();
            }
            return View(supplierGroupModels);
        }

        // GET: SupplierGroupModels/Create
        [Authorize(Roles = "Admin,IT,PPC")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: SupplierGroupModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin,IT,PPC")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GroupName,ID,Description")] SupplierGroupModels supplierGroupModels)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.SupplierGroupModels.Add(supplierGroupModels);
                    db.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    var sqlException = ex.InnerException.InnerException as SqlException;
                    if (sqlException != null && sqlException.Number == 2627)
                    {
                        //Response.Write("LeterNo already registered!");
                        ViewBag.error = "Group already registered!";
                        ModelState.AddModelError("", "Group already registered!");
                        return View(supplierGroupModels);
                    }
                }
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("","Data Error!");
            return View(supplierGroupModels);
        }

        // GET: SupplierGroupModels/Edit/5
        [Authorize(Roles = "Admin,IT,PPC")]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupplierGroupModels supplierGroupModels = db.SupplierGroupModels.Where(x => x.GroupName == id).FirstOrDefault();
            if (supplierGroupModels == null)
            {
                return HttpNotFound();
            }
            return View(supplierGroupModels);
        }

        // POST: SupplierGroupModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin,IT,PPC")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GroupName,Description")] SupplierGroupModels supplierGroupModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(supplierGroupModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Data Error!");
            return View(supplierGroupModels);
        }

        // GET: SupplierGroupModels/Delete/5
        [Authorize(Roles = "Admin,IT,PPC")]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupplierGroupModels supplierGroupModels = db.SupplierGroupModels.Find(id);
            if (supplierGroupModels == null)
            {
                return HttpNotFound();
            }
            return View(supplierGroupModels);
        }

        // POST: SupplierGroupModels/Delete/5
        [Authorize(Roles = "Admin,IT,PPC")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            SupplierGroupModels supplierGroupModels = db.SupplierGroupModels.Find(id);
            db.SupplierGroupModels.Remove(supplierGroupModels);
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
