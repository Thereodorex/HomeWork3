using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TmpTest.Models;

namespace TmpTest.DataContext
{
    public class FilesDBContext : DbContext
    {
        public FilesDBContext(DbContextOptions<MaterialsDBContext> options)
            : base(options)
        {
        }

        public DbSet<FileModel> files { get; set; }
    }
}