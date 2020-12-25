using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TmpTest.Models
{
    public class CategoryModel
    {
        [Key]
        public int Id { get; set;  }
        public String CategoryName { get; set; }
    }
}
