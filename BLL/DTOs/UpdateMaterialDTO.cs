using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class UpdateMaterialDTO
    {
        public int MaterialId { get; set; }
        public byte[] fileBlob { get; set; }
    }
}
