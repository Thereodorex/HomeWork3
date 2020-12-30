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

        public FilesRep(MaterialsDBContext context)
        {
            _context = context;
        }

        public List<FileModel> GetAll()
        {
            return _context.Files.ToList();
        }

        public FileModel GetById(int id)
        {
            return _context.Files.Where(c => c.FileId == id).FirstOrDefault();
        }

        public List<FileModel> GetByMaterialId(int id)
        {
            return _context.Files.Where(c => c.MaterialId == id).ToList();
        }

        public string GetContent(int id)
        {
            FileModel file = GetById(id);
            string fullPath = Path.GetFullPath(file.Path);
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

        public FileModel Append(int materialId, int version, ulong size, string name)
        {
            FileModel file = new FileModel();
            file.MaterialId = materialId;
            file.Name = name;
            file.Version = version;
            file.Size = size;
            file.FileId = _context.Files.Count() > 0 ? _context.Files.Max(f => f.FileId) + 1 : 1;
            file.Path = @$"Files\{file.MaterialId}-{file.FileId}";
            file.UploadTime = DateTime.Now;
            _context.Add(file);
            _context.SaveChanges();
            /*try
            {
                string fullPath = Path.GetFullPath(file.Path);
                using (FileStream fs = File.Create(fullPath))
                { 
                    Byte[] title = new UTF8Encoding(true).GetBytes(file.Content);
                    fs.Write(title, 0, title.Length);
                }
            }
            catch (Exception e)
            {
            }*/
            return file;
        }
    }
}

