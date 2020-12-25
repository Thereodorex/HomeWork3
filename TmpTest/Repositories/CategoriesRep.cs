using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TmpTest.Models;
using TmpTest.DataContext;

namespace TmpTest.Repositories
{
    public class CategoriesRep
    {
        private MaterialsDBContext _context;
        private List<CategoryModel> categories;

        public CategoriesRep(MaterialsDBContext context)
        {
            _context = context;
        }

        private void getData()
        {
            try
            {
                 categories = _context.Categories.ToList();
            }
            catch (Exception e)
            {
            }
        }

        private void updateData()
        {
            _context.SaveChanges();
        }

        public List<CategoryModel> GetAll()
        {
            getData();
            return categories;
        }

        public CategoryModel GetById(int id)
        {
            getData();
            List<CategoryModel> result = categories.Where(c => c.Id == id).ToList();
            return result.Count > 0 ? result[0] : null;
        }
    }
}
