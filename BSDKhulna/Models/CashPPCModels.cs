using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BSDKhulna.Models
{
    public class CashPPCModels
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }
        [Required]
        public string TenderNo { get; set; }
        [Required]
        public string DescriptionOfItem { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PPCDate { get; set; }
        [Required]
        public int TotalPages { get; set; }
        [Required]
        public int TotalAmount { get; set; }
        public string IDCardNo { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string NameOfTheFirm { get; set; }
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

    }
}