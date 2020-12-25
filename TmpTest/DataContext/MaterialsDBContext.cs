using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TmpTest.Models;

namespace TmpTest.DataContext
{
    public class MaterialsDBContext : DbContext
    {
        public MaterialsDBContext(DbContextOptions<MaterialsDBContext> options)
            : base(options)
        {
        }

        public DbSet<MaterialModel> Materials { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<FileModel> Files { get; set; }
    }
}
