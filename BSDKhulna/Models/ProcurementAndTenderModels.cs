using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BSDKhulna.Models
{
    public class ProcurementAndTenderModels
    {
        //IT Section 10
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }
        [Display(Name = "Letter No")]
        [Required]
        public string LetterNo { get; set; }
        [Required]
        public string From { get; set; }
        [Required]
        public string User { get; set; }
        [Required]
        public string Department { get; set; }// type
        [Display(Name = "Description Of Item")]
        [Required]
        [DataType(DataType.MultilineText)]
        public string ItemDescription { get; set; }
        [Required]
        public string Quantity { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ITDate { get; set; }
        [Required]
        public string Deno { get; set; }
        public string ITReferenceURL { get; set; }

        //CST section 12
        [Display(Name = "Tender No")]
        public string TenderNo { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? CSTDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? OpeningDate { get; set; }
        public string Remarks { get; set; }
        public string SpecURL { get; set; }
        public string NoticeURL { get; set; }
        public int? NumberOfPages { get; set; }
        public int? price { get; set; }

        [DataType(DataType.MultilineText)]
        public string CSTForwardedTo { get; set; }
        public string CSTAction { get; set; }
        public bool TenderNew { get; set; }
        public string TypeOfTender { get; set; }

        // LP Sectino 15
        public string ApprovalLetterNo { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? LPDate { get; set; }
        public string PurchaseType { get; set; }
        [DataType(DataType.MultilineText)]
        public string DescriptionOfItems { get; set; }
        public string PurchaseOrderNo { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? OrderDate { get; set; }
        public int? LPQuantity { get; set; }
        public int? LPAmount { get; set; }
        public string lpDeno { get; set; }
        public int? Days { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? LastDateOfSupply { get; set; }
        public string NameOfTheSupplier { get; set; }
        public string PlaceOfDelivery { get; set; }
        public string TimeExtentionUpto { get; set; }
        public string LPReferenceURL { get; set; }

        ///////CR Section 9
        public string CRPurchaseOrderNo { get; set; }
        public string CRNo { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? CRDate { get; set; }
        public string D44BNo { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? D44BRecievingDate { get; set; }
        public string RecievingQuantity { get; set; }
        public string CRDeno { get; set; }
        public string RequisitionNo { get; set; }
        public string group { get; set; }
        /////////CINS/ACIS Section 7
        public string CINSPurchaseOrderNo { get; set; }
        public string CINSCRNo { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? CINSDateOfRecieving { get; set; }
        public string CINSRecievingQuantity { get; set; }
        public string CINSDeno { get; set; }
        public string CINSDisposal { get; set; }
        [DataType(DataType.MultilineText)]
        public string CINSRemarks { get; set; }
        //////////////BillSection 9
        public string BillPurchaseOrderNo { get; set; }
        public string BillCRNo { get; set; }
        public string FinancialCode { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? BillRecievingDate { get; set; }
        public string BillForwardingPlace { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? BillForwardingDate { get; set; }
        [DataType(DataType.MultilineText)]
        public string BillRemarks { get; set; }
        public string BillTotalAmount { get; set; }
        public bool BFFC { get; set; }
        public string BillType { get; set; }


    }
}