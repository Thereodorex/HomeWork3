using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class NewMaterialDTO
    {
        public string MaterialName { get; set; }
        public string FileName { get; set; }
        public string Category { get; set; }
        public byte[] fileBlob { get; set; }
    }
}
