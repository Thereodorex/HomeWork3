using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataContext = DAL.DataContext;
using FileModel = DAL.Models.FileModel;
using MaterialModel = DAL.Models.MaterialModel;
using BLL.DTOs;

namespace BLL
{
    public class MaterialService : IMaterialService
    {
        private DataContext _context;
        private FileService _fileService;

        public MaterialService(DataContext context)
        {
            _context = context;
            _fileService = new FileService(context);
        }

        public List<MaterialModel> GetMaterials()
        {
            return _context.Materials.ToList();
        }

        public List<FileModel> GetMaterialVersions(int materialId)
        {
            return _context.Files.Where(f => f.MaterialId == materialId).ToList();
        }
        public FileModel GetMaterialVersion(int materialId, int version)
        {
            return _context.Files.Where(f => f.MaterialId == materialId).Where(f => f.Version == version).FirstOrDefault();
        }

        public FileModel GetMaterialLastVersion(int materialId)
        {
            var files = _context.Files.Where(f => f.MaterialId == materialId);
            int version = files.Max(f => f.Version);
            return files.Where(f => f.Version == version).FirstOrDefault();
        }

        public List<MaterialModel> GetMaterialsByCategory(string category)
        {
            return _context.Materials.Where(c => c.Category == category).ToList();
        }
        public MaterialModel GetMaterialById(int id)
        {
            return _context.Materials.Where(c => c.Id == id).FirstOrDefault();
        }
        public byte[] DownloadMaterial(int id, int version)
        {
            FileModel file = _context.Files.Where(f => f.MaterialId == id && f.Version == version).FirstOrDefault();
            return _fileService.DownloadFile(file.Id);
        }

        public FileModel CreateMaterial(NewMaterialDTO data)
        {
            string name = data.FileName;
            string category = data.Category;
            ulong size = (ulong)data.fileBlob.Length;
            int version = 1;
            MaterialModel material = new MaterialModel();
            material.Name = name;
            material.Category = category;
            if (_context.Materials.Count() > 0)
                material.Id = _context.Materials.Max(f => f.Id) + 1;
            else
                material.Id = 1;
            // materials.Add(material);
            _context.Add(material);
            FileModel file = new FileModel();
            file.MaterialId = material.Id;
            file.Version = version;
            file.Size = size;
            file.Id = _context.Files.Count() > 0 ? _context.Files.Max(f => f.Id) + 1 : 1;
            file.Path = _fileService.UploadFile(material, file, data.fileBlob);
            file.UploadTime = DateTime.Now;
            _context.Add(file);
            _context.SaveChanges();
            return file;
        }

        public FileModel UpdateMaterial(UpdateMaterialDTO data)
        {
            MaterialModel material = _context.Materials.Where(m => m.Id == data.MaterialId).FirstOrDefault();
            FileModel file = new FileModel();
            file.MaterialId = material.Id;
            file.Version = _context.Files.Where(f => f.MaterialId == material.Id).Max(f => f.Version) + 1;
            file.Size = (ulong)data.fileBlob.Length;
            file.Id = _context.Files.Count() > 0 ? _context.Files.Max(f => f.Id) + 1 : 1;
            file.Path = _fileService.UploadFile(material, file, data.fileBlob);
            file.UploadTime = DateTime.Now;
            _context.Add(file);
            _context.SaveChanges();
            return file;
        }


        public void ChangeMaterialCategory(int materialId, string category)
        {
            MaterialModel material = _context.Materials.Where(m => m.Id == materialId).FirstOrDefault();
            material.Category = category;
            _context.SaveChanges();
        }
    }
}
