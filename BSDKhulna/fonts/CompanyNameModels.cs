using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BSDKhulna.fonts
{
    public class CompanyNameModels
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string OwnerName { get; set; }
        [Required]
        public string Address { get; set; }
    }
}