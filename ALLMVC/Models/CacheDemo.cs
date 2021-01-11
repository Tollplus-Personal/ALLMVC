using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ALLMVC.Models
{
    public class CacheDemo
    {
        [Key]
        public DateTime LastUpdated { get; set; }
    }
}
