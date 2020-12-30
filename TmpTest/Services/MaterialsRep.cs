using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TmpTest.Models;
using TmpTest.DataContext;

namespace TmpTest.Repositories
{
    public class MaterialsRep
    {
        private MaterialsDBContext _context;

        public MaterialsRep(MaterialsDBContext context)
        {
            _context = context;
        }

        public List<MaterialModel> GetAll()
        {
            return _context.Materials.ToList();
        }

        public MaterialModel GetById(int id)
        {
            return _context.Materials.Where(c => c.MaterialId == id).FirstOrDefault();
        }

        public MaterialModel Append(string name, int categoryId)
        {
            MaterialModel material = new MaterialModel();
            material.Name = name;
            material.CategoryId = categoryId;
            if (_context.Materials.Count() > 0)
                material.MaterialId = _context.Materials.Max(f => f.MaterialId) + 1;
            else
                material.MaterialId = 1;
            // materials.Add(material);
            _context.Add(material);
            _context.SaveChanges();
            return material;
        }

        public void ChangeCategory(int materialId, int categoryId)
        {
            MaterialModel material = GetById(materialId);
            material.CategoryId = categoryId;
            _context.SaveChanges();
        }
    }
}
