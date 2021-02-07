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
    public class FileService : IFileService
    {
        private DataContext _context;

        public FileService(DataContext context)
        {
            _context = context;
        }
        public byte[] DownloadFile(int id)
        {
            FileModel file = _context.Files.Where(f => f.Id == id).FirstOrDefault();
            return System.IO.File.ReadAllBytes(@$"{file.Path}");
            //Stream stream = System.IO.File.OpenRead(@$"{file.Path}");
            //return File(stream, "application/octet-stream", file.Name);
        }
        public string UploadFile(MaterialModel material, FileModel fileModel, byte[] file)
        {
            string path = $"Files\\{material.Id}-{fileModel.Id}";
            File.WriteAllBytes(path, file);
                /*using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    file.CopyToAsync(fileStream);
                }*/
            return path;
        }
    }
}
