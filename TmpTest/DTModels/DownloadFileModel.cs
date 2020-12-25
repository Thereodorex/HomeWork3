using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TmpTest.DTModels
{
    public class DownloadFileModel
    {
        public string MaterialName { get; set; }

        public string FileName { get; set; }
        public int Version { get; set; }
        public string Content { get; set; }
    }
}
