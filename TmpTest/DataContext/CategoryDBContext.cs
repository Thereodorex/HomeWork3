using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TmpTest.Models;

namespace TmpTest.DataContext
{
    public class CategoryDBContext : DbContext
    {
        public CategoryDBContext(DbContextOptions<MaterialsDBContext> options)
            : base(options)
        {
        }

        public DbSet<CategoryModel> Categories { get; set; }
    }
}
