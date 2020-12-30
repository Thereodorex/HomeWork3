using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TmpTest.Models
{
    public class FileModel
    {
        [Key]
        public int FileId { get; set; }
        public int MaterialId { get; set; }
        public int Version { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public DateTime UploadTime { get; set; }
        public ulong Size { get; set; }
    }
}
