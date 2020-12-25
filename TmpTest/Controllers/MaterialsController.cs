using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TmpTest.Repositories;
using TmpTest.Models;
using TmpTest.DataContext;
using TmpTest.DTModels;
using Newtonsoft.Json.Linq;
using System.Text.Json;
//using Newtonsoft.Json;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TmpTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialsController : ControllerBase
    {
        private CategoriesRep categoriesRep;
        private MaterialsRep materialsRep;
        private FilesRep filesRep;
        private MaterialsDBContext _context;

        public MaterialsController(MaterialsDBContext context)
        {
            _context = context;
            categoriesRep = new CategoriesRep(context);
            materialsRep = new MaterialsRep(context);
            filesRep = new FilesRep(context);
        }

        [HttpGet("categories")]
        public IEnumerable<CategoryModel> GetCategories()
        {
            var categories = categoriesRep.GetAll();
            return categories;
        }

        // GET: api/<MaterialsController>
        [HttpGet]
        public IEnumerable<MaterialModel> Get()
        {
            return materialsRep.GetAll();
        }

        // GET api/<MaterialsController>/5
        [HttpGet("{id}")]
        public MaterialModel Get(int id)
        {
            return materialsRep.GetById(id);
        }

        [HttpGet("{id}/files")]
        public IEnumerable<FileModel> GetMaterialFiles(int id)
        {
            //return filesRep.GetAll().Where(f => f.MaterialId == id).ToList();
            return filesRep.GetByMaterialId(id);
        }

        [HttpGet("{id}/files/download")]
        public DownloadFileModel GetLastVersion(int id)
        {
            List<FileModel> files = filesRep.GetAll().Where(f => f.MaterialId == id).ToList();
            int max = files.Max(f => f.Version);
            return GetVersion(id, files.First(f => f.Version == max).Version);
        }

        [HttpGet("{id}/files/download/{version}")]
        public DownloadFileModel GetVersion(int id, int version)
        {
            DownloadFileModel model = new DownloadFileModel();
            List<FileModel> files = filesRep.GetAll().Where(f => f.MaterialId == id).ToList();
            if (files.Count == 0) return null;
            FileModel file = files.First(f => f.Version == version);
            string content = filesRep.GetContent(file.FileId);
            model.MaterialName = materialsRep.GetById(file.MaterialId).Name;
            model.FileName = file.Name;
            model.Version = file.Version;
            model.Content = content;
            return model;
        }

        [HttpGet("{id}/changeCategory/{categoryId}")]
        public void ChangeCategory(int id, int categoryId)
        {
            materialsRep.ChangeCategory(id, categoryId);
        }

        //[HttpGet("{id}/update/{name}_{size}")]
        [HttpPost("{id}/update")]
        public string UpdateMaterial(int id, [FromBody] UpdateMaterialModel content)
        {
            try
            {
                string name = content.FileName;
                int materialId = id;
                ulong size = content.Size;
                string data = content.Content;
                int version = filesRep.GetByMaterialId(materialId).Count + 1;
                if (version == 1)
                    return $"Material with id {materialId} does not exist";
                filesRep.Append(materialId, version, size, name, data);
                return "Ok";
            }
            catch (Exception e)
            {
                return "NotOk";
            }
        }

        /*[HttpPost("new/{categoryId}_{materialName}_{fileName}_{size}")]
        public void NewMaterial(int categoryId, string materialName, string fileName, ulong size)
        {
            int version = 1;
            MaterialModel material = materialsRep.Append(materialName, categoryId);
            filesRep.Append(material.MaterialId, version, size, fileName);
        }*/

        [HttpPost("new")]
        public string NewMaterial([FromBody] NewMaterialModel content)
        {
            try
            {
                //dynamic JSONstring = JObject.Parse(body.ToString());
                //NewMaterialModel content = JsonSerializer.Deserialize<NewMaterialModel>(JSONstring);
                string name = content.FileName;
                int categoryId = content.CategoryId;
                ulong size = content.Size;
                int version = 1;
                string data = content.Content;
                MaterialModel material = materialsRep.Append(name, categoryId);
                filesRep.Append(material.MaterialId, version, size, name, data);
                return "Ok";
            }
            catch (Exception e)
            {
                return "NotOk";
            }
        }
    }
}
