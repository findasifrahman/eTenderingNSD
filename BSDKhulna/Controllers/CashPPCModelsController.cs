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
    public class CashPPCModelsController : Controller
    {
        private AdminDBContext db = new AdminDBContext();

        // GET: CashPPCModels
        [Authorize(Roles = "Admin,IT,PPC")]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ////////////
            ViewBag.CurrentSort = sortOrder;
            ViewBag.IDSortParm = sortOrder == "TenderNo" ? "TenderNo_asc" : "TenderNo";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            var students = from s in db.CashPPCModels select s;
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
                students = students.Where(s => s.ID == ff || s.TenderNo.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "TenderNo_asc":
                    students = students.OrderBy(s => s.TenderNo);
                    break;
                case "TenderNo":
                    students = students.OrderByDescending(s => s.TenderNo);
                    break;
                default:  // Name ascending 
                    students = students.OrderByDescending(s => s.TenderNo);
                    break;
            }

            int pageSize = 50;
            int pageNumber = (page ?? 1);
            /////////////////////////////////////////////////////////
            return View(students.ToPagedList(pageNumber, pageSize));//db.AccountVoucherModels.ToList());
            ////////////
            //return View(db.CashPPCModels.ToList());
        }
      

        [Authorize(Roles = "Admin,IT,PPC")]
        public ActionResult PPCCopySalesReport()
        {
            ViewBag.visible = true;

            ViewBag.from = "";
            ViewBag.to = "";
            ViewBag.amount = "";

            DateTime date = DateTime.Now;
            var first = new DateTime(date.Year, date.Month, 1);

            return View(db.CashPPCModels.Where(x => DbFunctions.TruncateTime(x.PPCDate) >= DbFunctions.TruncateTime(first) && DbFunctions.TruncateTime(x.PPCDate) <= DbFunctions.TruncateTime(DateTime.Now)).ToList());
        }
        [Authorize(Roles = "Admin,IT,PPC")]
        public ActionResult PPCCopySalesReportPrint(string dt1, string dt2)
        {

            ViewBag.from = "From: " + dt1;
            ViewBag.to = "To: " + dt2;


            DateTime tt1 = Convert.ToDateTime(dt1);
            DateTime tt2 = Convert.ToDateTime(dt2);
            ViewBag.amount = "Total Amount: " + (db.CashPPCModels.Where(x => DbFunctions.TruncateTime(x.PPCDate) >= DbFunctions.TruncateTime(tt1) && DbFunctions.TruncateTime(x.PPCDate) <= DbFunctions.TruncateTime(tt2)).Sum(x => (int?)x.TotalAmount) ?? 0).ToString();
            var mod = db.CashPPCModels.Where(x => DbFunctions.TruncateTime(x.PPCDate) >= DbFunctions.TruncateTime(tt1) && DbFunctions.TruncateTime(x.PPCDate) <= DbFunctions.TruncateTime(tt2));
            return View("PPCCopySalesReport", mod);
        }

        [Authorize(Roles = "Admin,IT,PPC")]
        public ActionResult PPCCopySalesReportDate(string command,string dt1, string dt2)
        {
            ViewBag.visible = false;

            ViewBag.from = "From: " + dt1;
            ViewBag.to = "To: " + dt2;


            DateTime tt1 = Convert.ToDateTime(dt1);
            DateTime tt2 = Convert.ToDateTime(dt2);
            ViewBag.amount = "Total Amount: " + (db.CashPPCModels.Where(x => DbFunctions.TruncateTime(x.PPCDate) >= DbFunctions.TruncateTime(tt1) && DbFunctions.TruncateTime(x.PPCDate) <= DbFunctions.TruncateTime(tt2)).Sum(x => (int?)x.TotalAmount) ?? 0).ToString() ;
            var mod = db.CashPPCModels.Where(x => DbFunctions.TruncateTime(x.PPCDate) >= DbFunctions.TruncateTime(tt1) && DbFunctions.TruncateTime(x.PPCDate) <= DbFunctions.TruncateTime(tt2));
            return View("PPCCopySalesReport", mod);
        }
        [Authorize(Roles = "Admin,IT,PPC")]
        // GET: CashPPCModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CashPPCModels cashPPCModels = db.CashPPCModels.Find(id);
            if (cashPPCModels == null)
            {
                return HttpNotFound();
            }
            return View(cashPPCModels);
        }
        [Authorize(Roles = "Admin,IT,PPC")]
        // GET: CashPPCModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CashPPCModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin,IT,PPC")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TenderNo,DescriptionOfItem,TotalPages,TotalAmount,IDCardNo,Name,NameOfTheFirm,Address")] CashPPCModels cashPPCModels)
        {
            if (ModelState.IsValid)
            {
                cashPPCModels.PPCDate = DateTime.Now;
                db.CashPPCModels.Add(cashPPCModels);
                db.SaveChanges();
                return RedirectToAction("Index");//"PPCCopySalesReport");
            }

            return View(cashPPCModels);
        }
        [Authorize(Roles = "Admin,IT,PPC")]
        // GET: CashPPCModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CashPPCModels cashPPCModels = db.CashPPCModels.Find(id);
            if (cashPPCModels == null)
            {
                return HttpNotFound();
            }
            return View(cashPPCModels);
        }

        // POST: CashPPCModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin,IT,PPC")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TenderNo,DescriptionOfItem,TotalPages,TotalAmount,IDCardNo,Name,NameOfTheFirm,Address")] CashPPCModels cashPPCModels)
        {
            if (ModelState.IsValid)
            {
                cashPPCModels.PPCDate = DateTime.Now;
                db.Entry(cashPPCModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cashPPCModels);
        }
        [Authorize(Roles = "Admin,IT,PPC")]
        // GET: CashPPCModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CashPPCModels cashPPCModels = db.CashPPCModels.Find(id);
            if (cashPPCModels == null)
            {
                return HttpNotFound();
            }
            return View(cashPPCModels);
        }
        [Authorize(Roles = "Admin,IT,PPC")]
        // POST: CashPPCModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CashPPCModels cashPPCModels = db.CashPPCModels.Find(id);
            db.CashPPCModels.Remove(cashPPCModels);
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
