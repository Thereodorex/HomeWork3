using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileModel = DAL.Models.FileModel;
using MaterialModel = DAL.Models.MaterialModel;
using BLL.DTOs;

namespace BLL
{
    interface IMaterialService
    {
        public List<MaterialModel> GetMaterials();
        public List<FileModel> GetMaterialVersions(int materialId);
        public FileModel GetMaterialVersion(int materialId, int version);
        public FileModel GetMaterialLastVersion(int materialId);
        public List<MaterialModel> GetMaterialsByCategory(string category);
        public MaterialModel GetMaterialById(int id);
        public byte[] DownloadMaterial(int id, int version);

        public FileModel CreateMaterial(NewMaterialDTO data);

        public FileModel UpdateMaterial(UpdateMaterialDTO data);


        public void ChangeMaterialCategory(int materialId, string category);
    }
}
