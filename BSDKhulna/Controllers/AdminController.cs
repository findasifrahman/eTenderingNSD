using BSDKhulna.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BSDKhulna.Controllers
{
    public class AdminController : Controller
    {
        private AdminDBContext db = new AdminDBContext();
        // GET: Admin
        public ActionResult Index()
        {
            ViewBag.cstApproaval = db.ProcurementAndTenderModels.Where(x => x.TenderNo != null && x.ApprovalLetterNo == null && x.PurchaseOrderNo == null).Count();
            ViewBag.notYetSupplied = db.ProcurementAndTenderModels.Where(x => x.TenderNo != null && x.ApprovalLetterNo != null && x.PurchaseOrderNo != null && x.CRNo == null && x.CRPurchaseOrderNo == null).Count();
            ViewBag.notYetInspected = db.ProcurementAndTenderModels.Where(x => x.TenderNo != null && x.ApprovalLetterNo != null && x.PurchaseOrderNo != null && x.CRNo != null && x.CRPurchaseOrderNo != null && x.CINSPurchaseOrderNo == null && x.CINSCRNo == null).Count();
            ViewBag.InspectedResult = db.ProcurementAndTenderModels.Where(x => x.TenderNo != null && x.ApprovalLetterNo != null && x.PurchaseOrderNo != null && x.CRNo != null && x.CRPurchaseOrderNo != null && x.CINSPurchaseOrderNo != null && x.CINSCRNo != null && x.BillPurchaseOrderNo == null && x.BillCRNo == null).Count();
            ViewBag.waitingForBill = db.ProcurementAndTenderModels.Where(x => x.TenderNo != null && x.ApprovalLetterNo != null && x.PurchaseOrderNo != null && x.CRNo != null && x.CRPurchaseOrderNo != null && x.CINSPurchaseOrderNo != null && x.CINSCRNo != null && x.BillPurchaseOrderNo == null && x.BillCRNo == null).Count();
            return View();
        }
    }
}