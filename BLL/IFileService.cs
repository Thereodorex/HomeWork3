using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileModel = DAL.Models.FileModel;
using MaterialModel = DAL.Models.MaterialModel;
using DataContext = DAL.DataContext;
using System.IO;

namespace BLL
{
    interface IFileService
    {
        public byte[] DownloadFile(int id);
        public string UploadFile(MaterialModel material, FileModel fileModel, byte[] file);
    }
}
