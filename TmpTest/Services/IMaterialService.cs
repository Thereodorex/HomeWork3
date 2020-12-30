using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TmpTest.Models;
using TmpTest.DataContext;


// Пока не используется
namespace TmpTest.Services
{
    public interface IMaterialService
    {

        public List<MaterialModel> GetMaterials();
        public List<MaterialModel> GetMaterialsByCategory(int categoryId);
        public MaterialModel GeMaterialtById(int id);

        public MaterialModel CreateMaterial(string name, int categoryId);

        public MaterialModel UpdateMaterial(int materialId, int categoryId);


        public void ChangeMaterialCategory(int materialId, int categoryId);
    }
}
