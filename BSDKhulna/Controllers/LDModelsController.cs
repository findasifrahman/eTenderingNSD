using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BSDKhulna.Models;
using PagedList;

namespace BSDKhulna.Controllers
{
    public class LDModelsController : Controller
    {
        private AdminDBContext db = new AdminDBContext();

        // GET: LDModels
        [Authorize(Roles = "Admin,IT,BILL")]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.IDSortParm = sortOrder == "PurchaseOrderNo" ? "PurchaseOrderNo_asc" : "PurchaseOrderNo";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            var students = from s in db.LDModels select s;
            DateTime dt = DateTime.Today.Date;
            try
            {
                dt = Convert.ToDateTime(searchString);
            }
            catch { }
            int ff = 0;
            try
            {
                ff = Convert.ToInt32(searchString);
            }
            catch { }
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.PurchaseOrderNo.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "PurchaseOrderNo_asc":
                    students = students.OrderBy(s => s.PurchaseOrderNo);
                    break;
                case "PurchaseOrderNo":
                    students = students.OrderByDescending(s => s.PurchaseOrderNo);
                    break;
                default:  // Name ascending 
                    students = students.OrderByDescending(s => s.ID);
                    break;
            }

            int pageSize = 50;
            int pageNumber = (page ?? 1);
            /////////////////////////////////////////////////////////
            return View(students.ToPagedList(pageNumber, pageSize));//db.AccountVoucherModels.ToList());

            return View(db.LDModels.ToList());
        }
        [Authorize(Roles = "Admin,IT,BILL")]
        // GET: LDModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LDModels lDModels = db.LDModels.Find(id);
            if (lDModels == null)
            {
                return HttpNotFound();
            }
            return View(lDModels);
        }

        // GET: LDModels/Create
        [Authorize(Roles = "Admin,IT,BILL")]
        public ActionResult Create()
        {
            ViewBag.PurchaseOrderNolist = new SelectList(db.ProcurementAndTenderModels.Where(x => x.PurchaseOrderNo != null).ToList(), "PurchaseOrderNo", "PurchaseOrderNo");
            return View();
        }

        // POST: LDModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin,IT,BILL")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,PurchaseOrderNo,LDReferenceNo,PODate,CompanyName,LDAmount,TotalAmount,Percent")] LDModels lDModels)
        {
            ViewBag.PurchaseOrderNolist = new SelectList(db.ProcurementAndTenderModels.Where(x => x.PurchaseOrderNo != null).ToList(), "PurchaseOrderNo", "PurchaseOrderNo");
            if (ModelState.IsValid)
            {
                db.LDModels.Add(lDModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lDModels);
        }

        // GET: LDModels/Edit/5
        [Authorize(Roles = "Admin,IT,BILL")]
        public ActionResult Edit(int? id)
        {
            ViewBag.PurchaseOrderNolist = new SelectList(db.ProcurementAndTenderModels.Where(x => x.PurchaseOrderNo != null).ToList(), "PurchaseOrderNo", "PurchaseOrderNo");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LDModels lDModels = db.LDModels.Find(id);
            if (lDModels == null)
            {
                return HttpNotFound();
            }
            return View(lDModels);
        }

        // POST: LDModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin,IT,BILL")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,PurchaseOrderNo,LDReferenceNo,PODate,CompanyName,LDAmount,TotalAmount,Percent")] LDModels lDModels)
        {
            ViewBag.PurchaseOrderNolist = new SelectList(db.ProcurementAndTenderModels.Where(x => x.PurchaseOrderNo != null).ToList(), "PurchaseOrderNo", "PurchaseOrderNo");
            if (ModelState.IsValid)
            {
                db.Entry(lDModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lDModels);
        }

        // GET: LDModels/Delete/5
        [Authorize(Roles = "Admin,IT,BILL")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LDModels lDModels = db.LDModels.Find(id);
            if (lDModels == null)
            {
                return HttpNotFound();
            }
            return View(lDModels);
        }

        // POST: LDModels/Delete/5
        [Authorize(Roles = "Admin,IT,BILL")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LDModels lDModels = db.LDModels.Find(id);
            db.LDModels.Remove(lDModels);
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
