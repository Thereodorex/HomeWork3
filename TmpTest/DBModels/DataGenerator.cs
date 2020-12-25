using TmpTest.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TmpTest.Models
{
    public class DataGenerator
    {

        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MaterialsDBContext(
                serviceProvider.GetRequiredService<DbContextOptions<MaterialsDBContext>>()))
            {
                // Look for any board games already in database.
                if (!context.Materials.Any())
                {
                }
                if (!context.Files.Any())
                {
                }
                if (context.Categories.Any())
                {
                    return;
                }
                context.Categories.AddRange(
                    new CategoryModel
                    {
                        Id = 3,
                        CategoryName = "presentation"
                    },
                    new CategoryModel
                    {
                        Id = 1,
                        CategoryName = "app"
                    },
                    new CategoryModel
                    {
                        Id = 2,
                        CategoryName = "other"
                    }
                    );
                context.SaveChanges();
            }
        }
    }
}
