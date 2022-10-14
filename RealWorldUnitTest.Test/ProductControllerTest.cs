using Microsoft.EntityFrameworkCore;
using RealWorldUnitTest.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealWorldUnitTest.Test
{
    public class ProductControllerTest
    {
        protected DbContextOptions<UnitTestDbContext> _contextOptions { get; set; }

        public void SetContextOptions(DbContextOptions<UnitTestDbContext> contextOptions)
        {
            _contextOptions = contextOptions;
        }

        public void Seed()
        {
            using (UnitTestDbContext context = new UnitTestDbContext(_contextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.Category.Add(new Category { Name = "Kalemler" });
                context.Category.Add(new Category { Name = "Defterler" });
                context.SaveChanges();

                context.Product.Add(new Product() { CategoryId = 1, Name = "Kalem 10", Price = 100, Stock = 100, Color = "Kırmızı" });
                context.Product.Add(new Product() { CategoryId = 1, Name = "Kalem 20", Price = 100, Stock = 100, Color = "Mavi" });
                context.SaveChanges();
            }
        }

    }
}
