using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BSDKhulna.Models
{
    public class GeneralNoticeModels
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string DescriptionOfNotice { get; set; }
        public string NoticeDownloadUrl { get; set; }
    }
}