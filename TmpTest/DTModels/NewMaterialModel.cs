using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace TmpTest.DTModels
{
    public class NewMaterialModel
    {
        public int CategoryId { get; set; }
        public string MaterialName { get; set; }

        public string FileName { get; set; }
        public ulong Size { get; set; }
        public string Content { get; set; }
    }
}
