using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BSDKhulna.Models
{
    public class SuplierUploadModels
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Display(Name = "Letter No")]
        [Required]
        [Key, Column(Order = 1)]
        public int LetterID { get; set; }
        [Required]
        [Key, Column(Order = 2)]
        public string SupplierID { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string SupplierContact { get; set; }
        [Required]
        public string Quantity { get; set; }
        [Required]
        public string Deno { get; set; }
        [Required]
        public int price { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        public string QuoteDocURL { get; set; }
        public string Other1DocURL { get; set; }
        public string Other2DocURL { get; set; }
        public string Other3DocURL { get; set; }
        public string Other4DocURL { get; set; }
    }
}