using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TmpTest.DTModels
{
    public class UpdateMaterialModel
    {
        public int MaterialId { get; set; }
        public string MaterialName { get; set; }

        public string FileName { get; set; }
        public ulong Size { get; set; }
        public string Content { get; set; }
    }
}
