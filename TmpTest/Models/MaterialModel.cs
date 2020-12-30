using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TmpTest.Models
{
    public class MaterialModel
    {
        [Key]
        public int MaterialId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
    }
}
