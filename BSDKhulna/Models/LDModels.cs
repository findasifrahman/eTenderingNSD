using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BSDKhulna.Models
{
    public class LDModels
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }
        [Required]
        public string PurchaseOrderNo { get; set; }
        [Required]
        public string LDReferenceNo { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PODate { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public int? LDAmount { get; set; }
        [Required]
        public int? TotalAmount { get; set; }
        public int? Percent { get; set; }
    }
}