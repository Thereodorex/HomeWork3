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
        private List<MaterialModel> materials;

        public MaterialsRep(MaterialsDBContext context)
        {
            _context = context;
        }

        private void getData()
        {
            try
            {
                materials = _context.Materials.ToList();
            }
            catch (Exception e)
            {
            }
        }

        private void updateData()
        {
            _context.SaveChanges();
        }

        public List<MaterialModel> GetAll()
        {
            getData();
            return materials;
        }

        public MaterialModel GetById(int id)
        {
            getData();
            List<MaterialModel> result = materials.Where(c => c.MaterialId == id).ToList();
            return result.Count > 0 ? result[0] : null;
        }

        public MaterialModel Append(string name, int categoryId)
        {
            getData();
            MaterialModel material = new MaterialModel();
            material.Name = name;
            material.CategoryId = categoryId;
            if (materials.Count > 0)
                material.MaterialId = materials.Max(f => f.MaterialId) + 1;
            else
                material.MaterialId = 1;
            // materials.Add(material);
            _context.Add(material);
            updateData();
            return material;
        }

        public void ChangeCategory(int materialId, int categoryId)
        {
            MaterialModel material = GetById(materialId);
            material.CategoryId = categoryId;
            updateData();
        }
    }
}
