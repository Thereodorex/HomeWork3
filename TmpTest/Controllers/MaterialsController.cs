using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TmpTest.Repositories;
using TmpTest.Models;
using TmpTest.DataContext;
using TmpTest.DTO;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using System.IO;
//using Newtonsoft.Json;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TmpTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialsController : ControllerBase
    {
        private MaterialsDBContext _context;
        private MaterialsRep _materialsRep;
        private FilesRep _filesRep;

        public MaterialsController(MaterialsDBContext context)
        {
            _context = context;
            _materialsRep = new MaterialsRep(context);
            _filesRep = new FilesRep(context);
        }

        [HttpGet("categories")]
        public IEnumerable<CategoryModel> GetCategories()
        {
            return _context.Categories.ToList();
        }

        [HttpGet("categories/{id}")]
        public IEnumerable<MaterialModel> GetMaterialsByCateogory(int id)
        {
            return _context.Materials.Where(m => m.CategoryId == id).ToList();
        }

        // GET: api/<MaterialsController>
        [HttpGet]
        public IEnumerable<MaterialModel> Get()
        {
            return _context.Materials.ToList();
        }

        // GET api/<MaterialsController>/5
        [HttpGet("{id}")]
        public MaterialModel Get(int id)
        {
            return _context.Materials.Where(m => m.MaterialId == id).FirstOrDefault();
        }

        [HttpGet("{id}/files")]
        public IEnumerable<FileModel> GetMaterialFiles(int id)
        {
            //return filesRep.GetAll().Where(f => f.MaterialId == id).ToList();
            return _context.Files.Where(f => f.MaterialId == id).ToList();
        }

        [HttpGet("{id}/files/{version}")]
        public GetFileInfoDTO GetFileInfo(int id, int version)
        {
            GetFileInfoDTO model = new GetFileInfoDTO();
            List<FileModel> files = _context.Files.Where(f => f.MaterialId == id).ToList();
            if (files.Count == 0) return null;
            FileModel file = files.First(f => f.Version == version);
            model.MaterialName = _context.Materials.Where(m => m.MaterialId == id).FirstOrDefault().Name;
            model.FileName = file.Name;
            model.Version = file.Version;
            model.Id = file.FileId;
            model.FileSize = file.Size;
            return model;
        }

        [HttpGet("{id}/files/last")]
        public int GetLastFileInfo(int id)
        {
            int version = _context.Files.Where(f => f.MaterialId == id).Max(f => f.Version);
            return version;
            //return GetFileInfo(id, version);
        }

        [HttpGet("{id}/files/{version}/download")]
        public async Task<IActionResult> DownloadFile(int id, int version)
        {
            GetFileInfoDTO model = new GetFileInfoDTO();
            List<FileModel> files = _context.Files.Where(f => f.MaterialId == id).ToList();
            if (files.Count == 0) return null;
            FileModel file = files.First(f => f.Version == version);
            Stream stream = System.IO.File.OpenRead(@$"{file.Path}");
            return File(stream, "application/octet-stream", file.Name);
        }

        [HttpGet("{id}/files/last/download")]
        public async Task<IActionResult> DownloadLastVersion(int id)
        {
            int version = _context.Files.Where(f => f.MaterialId == id).Max(f => f.Version);
            return await DownloadFile(id, version);
        }


        [HttpPost("{id}/changeCategory")]
        public void ChangeCategory(int id, [FromForm] int categoryId)
        {
            MaterialModel material = _context.Materials.Where(m => m.MaterialId == id).FirstOrDefault();
            material.CategoryId = categoryId;
            _context.SaveChanges();
        }

        [HttpPost("new")]
        public string NewMaterial([FromForm] NewMaterialDTO fileInfo, [FromForm]IFormFile file)
        {
            try
            {
                //dynamic JSONstring = JObject.Parse(body.ToString());
                //NewMaterialModel content = JsonSerializer.Deserialize<NewMaterialModel>(JSONstring);
                string name = fileInfo.FileName;
                int categoryId = fileInfo.CategoryId;
                ulong size = (ulong)file.Length;
                int version = 1;
                MaterialModel material = _materialsRep.Append(name, categoryId);
                FileModel fileModel = _filesRep.Append(material.MaterialId, version, size, name);
                using (var fileStream = new FileStream($"Files\\{material.MaterialId}-{fileModel.FileId}", FileMode.Create))
                {
                    file.CopyToAsync(fileStream);
                }

                return "Ok";
            }
            catch (Exception e)
            {
                return "NotOk";
            }
        }

        [HttpPost("update")]
        public string UpdateMaterial([FromForm] UpdateMaterialDTO fileInfo, [FromForm] IFormFile file)
        {
            int id = 1;
            try
            {
                //dynamic JSONstring = JObject.Parse(body.ToString());
                //NewMaterialModel content = JsonSerializer.Deserialize<NewMaterialModel>(JSONstring);
                string name = fileInfo.FileName;
                ulong size = (ulong)file.Length;
                MaterialModel material = _context.Materials.Where(m => m.MaterialId == id).FirstOrDefault();
                int version = _context.Files.Where(f => f.MaterialId == id).Max(f => f.Version) + 1;
                FileModel fileModel = _filesRep.Append(material.MaterialId, version, size, name);
                using (var fileStream = new FileStream($"Files\\{material.MaterialId}-{fileModel.FileId}", FileMode.Create))
                {
                    file.CopyToAsync(fileStream);
                }

                return fileInfo.FileName;
            }
            catch (Exception e)
            {
                return "NotOk";
            }
        }
    }
}
