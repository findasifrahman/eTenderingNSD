using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BSDKhulna.Models
{
    public class SupplierModels
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Key]
        [Required]
        [RegularExpression(@"^[^<>.,?;:'()!~%-_@#%/*""\s]+$", ErrorMessage = "No white space and speciel char allowed")]
        public string SupplierName { get; set; }
        [Required]
        public string SupplierGroup { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string CellNumber { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string password { get; set; }
        [Required]
        public string BarCodeNumber { get; set; }
        [Required]
        public string IDCardNumber { get; set; }
        public string pictureURL { get; set; }

        public bool Published { get; set; }
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }



    }
}