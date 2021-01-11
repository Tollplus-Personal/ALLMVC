using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ALLMVC.Models
{
    public class CourseCatalog
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string CourseName { get; set; }
        public decimal Cost { get; set; }
        [DataType(DataType.Upload)]
        public string FileName { set; get; }
    }
}
