using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace RecipeBox
{
    public class CategoryTest : IDisposable
    {
        public CategoryTest()
        {
            DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=recipe_box_test;Integrated Security=SSPI;";
        }

        [Fact]
        public void Test_CategoryEmptyAtFirst()
        {
            //Arrange, Act
            int result = Category.GetAll().Count;

            //Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void Test_Equal_ReturnsTrueForSameName()
        {
          //Arrange, Act
          Category firstCategory = new Category("Mexican");
          Category secondCategory = new Category("Mexican");

          //Assert
          Assert.Equal(firstCategory, secondCategory);
        }

        public void Dispose()
        {
            //  Recipe.DeleteAll();
            //  Category.DeleteAll();
        }
    }
}
