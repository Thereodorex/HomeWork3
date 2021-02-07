using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DAL;
using DAL.Models;
using BLL;
using BLL.DTOs;
using System.IO;

namespace Layers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialsController : ControllerBase
    {
        //private MaterialsDBContext _context;
        private MaterialService _materialService;
        private DataContext _context;

        public MaterialsController(DataContext context)
        {
            _materialService = new MaterialService(context);
            _context = context;
        }

        // GET: api/<MaterialsController>
        [HttpGet]
        public IEnumerable<MaterialModel> Get()
        {
            return _materialService.GetMaterials();
        }

        // GET api/<MaterialsController>/5
        [HttpGet("{id}")]
        public MaterialModel Get(int id)
        {
            return _materialService.GetMaterialById(id);
        }

        [HttpGet("{id}/files")]
        public IEnumerable<FileModel> GetMaterialFiles(int id)
        {
            //return filesRep.GetAll().Where(f => f.MaterialId == id).ToList();
            return _materialService.GetMaterialVersions(id);
        }

        [HttpGet("{id}/{version}/info")]
        public FileModel GetFileInfo(int id, int version)
        {
            return _materialService.GetMaterialVersion(id, version);
        }

        [HttpGet("{id}/info")]
        public FileModel GetLastFileInfo(int id)
        {
            return _materialService.GetMaterialLastVersion(id);
        }

        [HttpGet("{id}/download/{version}")]
        public FileContentResult DownloadFile(int id, int version)
        {
            var material = _materialService.GetMaterialById(id);
            return File(_materialService.DownloadMaterial(id, version), "application/octet-stream", material.Name);
        }

        [HttpGet("{id}/download")]
        public FileContentResult DownloadLastVersion(int id)
        {
            var material = _materialService.GetMaterialById(id);
            var file = GetLastFileInfo(id);
            return File(_materialService.DownloadMaterial(id, file.Version), "application/octet-stream", material.Name);
        }


        [HttpPost("{id}/changeCategory")]
        public void ChangeCategory(int id, [FromForm] string category)
        {
            _materialService.ChangeMaterialCategory(id, category);
        }

        [HttpPost("new")]
        public string NewMaterial([FromForm] NewMaterialDTO fileInfo, [FromForm] IFormFile file)
        {
            try
            {
                byte[] bytes;
                using (var stream = new MemoryStream())
                {
                    file.CopyTo(stream);
                    bytes = stream.ToArray();
                }
                fileInfo.fileBlob = bytes;
                _materialService.CreateMaterial(fileInfo);

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
            try
            {
                byte[] bytes;
                using (var stream = new MemoryStream())
                {
                    file.CopyTo(stream);
                    bytes = stream.ToArray();
                }
                fileInfo.fileBlob = bytes;
                _materialService.UpdateMaterial(fileInfo);

                return "Ok";
            }
            catch (Exception e)
            {
                return "NotOk";
            }
        }
    }
}
