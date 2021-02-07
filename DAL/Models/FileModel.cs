using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class FileModel
    {
        public int Id { get; set; }
        public int MaterialId { get; set; }
        public int Version { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public DateTime UploadTime { get; set; }
        public ulong Size { get; set; }
    }
}
