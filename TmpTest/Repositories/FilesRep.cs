using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TmpTest.Models;
using TmpTest.DataContext;
using System.Text;

namespace TmpTest.Repositories
{
    public class FilesRep
    {
        private MaterialsDBContext _context;
        private List<FileModel> files;

        public FilesRep(MaterialsDBContext context)
        {
            _context = context;
        }

        private void getData()
        {
            try
            {
                files = _context.Files.ToList();
            }
            catch (Exception e)
            {
            }
        }

        private void updateData()
        {
            _context.SaveChanges();
        }

        public List<FileModel> GetAll()
        {
            getData();
            return files;
        }

        public FileModel GetById(int id)
        {
            getData();
            List<FileModel> result = files.Where(c => c.FileId == id).ToList();
            return result.Count > 0 ? result[0] : null;
        }

        public List<FileModel> GetByMaterialId(int id)
        {
            getData();
            List<FileModel> result = files.Where(c => c.MaterialId == id).ToList();
            return result;
        }

        public string GetContent(int id)
        {
            Console.WriteLine(id);
            FileModel file = GetById(id);
            string fullPath = Path.GetFullPath(file.Path);
            Console.WriteLine(fullPath);
            try {
                //return System.IO.File.ReadAllText(fullPath);
                using (FileStream fstream = File.OpenRead(fullPath))
                {
                    byte[] array = new byte[fstream.Length];
                    fstream.Read(array, 0, array.Length);
                    string content = System.Text.Encoding.Default.GetString(array);
                    Console.WriteLine(content);
                    return content;
                }
            }
            catch (Exception e)
            {
                return "";
            }
        }

        public void Append(int materialId, int version, ulong size, string name, string Data)
        {
            getData();
            FileModel file = new FileModel();
            file.MaterialId = materialId;
            file.Name = name;
            file.Version = version;
            file.Size = size;
            file.FileId = files.Count > 0 ? files.Max(f => f.FileId) + 1 : 1;
            file.Path = @$"Files\{file.MaterialId}-{file.FileId}";
            file.UploadTime = DateTime.Now;
            file.Content = Data;
            //files.Add(file);
            _context.Add(file);
            updateData();
            try
            {
                /*using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.Default))
                {
                    sw.Write(file.Content);
                }*/
                string fullPath = Path.GetFullPath(file.Path);
                using (FileStream fs = File.Create(fullPath))
                {
                    // Add some text to file    
                    Byte[] title = new UTF8Encoding(true).GetBytes(file.Content);
                    fs.Write(title, 0, title.Length);
                }
            }
            catch (Exception e)
            {
            }
        }
    }
}

