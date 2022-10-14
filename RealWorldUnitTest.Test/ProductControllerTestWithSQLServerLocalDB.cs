using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealWorldUnitTest.Web.Controllers;
using RealWorldUnitTest.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RealWorldUnitTest.Test
{
    public class ProductControllerTestWithSQLServerLocalDB : ProductControllerTest
    {
        public ProductControllerTestWithSQLServerLocalDB()
        {
            var sqlConnection =  @"Server=(localdb)\MSSQLLocalDB;Database=TestDb,Trusted_Connection=true,MultipleActiveResultSets=true";

            SetContextOptions(new DbContextOptionsBuilder<UnitTestDbContext>().UseSqlServer(sqlConnection).Options);
        }

        [Fact]
        public async Task Create_ModelValidProduct_ReturnRedirectToActionWithSaveProduct()
        {
            var newProduct = new Product { Name = "Kalem 30", Price = 200, Stock = 100,Color="Mavi" };
            using (var context = new UnitTestDbContext(_contextOptions))
            {
                var category = context.Category.First();

                newProduct.CategoryId = category.Id;

                //var repository = new repository<Product>(context)
                var controller = new ProductsController(context);

                var result = await controller.Create(newProduct);

                var redirect = Assert.IsType<RedirectToActionResult>(result);

                Assert.Equal("Index", redirect.ActionName);

            }

            using (var context = new UnitTestDbContext(_contextOptions))
            {
                var product = context.Product.FirstOrDefault(x => x.Name == newProduct.Name);

                Assert.Equal(newProduct.Name, product.Name);
            }
        }



        [Theory]
        [InlineData(1)]
        public async Task DeleteCategory_ExistCategoryId_DeletedAllProducts(int categoryId)
        {
            using (var context = new UnitTestDbContext(_contextOptions))
            {
                var category = await context.Category.FindAsync(categoryId);

                context.Category.Remove(category);

                context.SaveChanges();
            }

            using (var context = new UnitTestDbContext(_contextOptions))
            {
                var products = await context.Product.Where(x => x.CategoryId == categoryId).ToListAsync();

                //Boş olamamsı lağzım yani verisilindiğinde ilişkileri silinmemesi gerekir
                Assert.Empty(products);
            }
        }



    }
}
