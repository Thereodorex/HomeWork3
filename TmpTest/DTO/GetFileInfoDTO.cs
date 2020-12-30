using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// не используется
namespace TmpTest.DTO
{
    public class GetFileInfoDTO
    {
        public int Id { get; set; }
        public string MaterialName { get; set; }

        public string FileName { get; set; }
        public ulong FileSize { get; set; }
        public int Version { get; set; }

    }
}
