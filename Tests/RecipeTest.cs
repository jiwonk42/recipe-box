using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace RecipeBox
{
    public class RecipeTest : IDisposable
    {
        public RecipeTest()
        {
            DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=recipe_box_test;Integrated Security=SSPI;";
        }

        [Fact]
        public void Test_DatabaseEmptyAtFirst()
        {
            //Arrange, Act
            int result = Recipe.GetAll().Count;

            //Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void Test_Equal_ReturnsTrueIfDescriptionsAreTheSame_true()
        {
            //Arrange, Act
            Recipe firstRecipe = new Recipe("Mac and cheese", "cheese and noodles", "cook it", 5);
            Recipe secondRecipe = new Recipe("Mac and cheese", "cheese and noodles", "cook it", 5);

            //Assert
            Assert.Equal(firstRecipe, secondRecipe);
        }

        [Fact]
        public void Test_Save_SavesToDatabase()
        {
            //Arrange
            Recipe testRecipe = new Recipe("Mac and cheese", "cheese and noodles", "cook it", 5);

            //Act
            testRecipe.Save();
            List<Recipe> result = Recipe.GetAll();
            List<Recipe> testList = new List<Recipe>{testRecipe};

            //Assert
            Assert.Equal(testList, result);
        }

        [Fact]
        public void Test_Save_AssignsIdToObject()
        {
            //Arrange
            Recipe testRecipe = new Recipe("Mac and cheese", "cheese and noodles", "cook it", 5);

            //Act
            testRecipe.Save();
            Recipe savedRecipe = Recipe.GetAll()[0];


            int result = savedRecipe.GetId();
            int testId = testRecipe.GetId();

            //Assert
            Assert.Equal(testId, result);
        }

        [Fact]
        public void Test_Find_FindRecipeInDatabase()
        {
            //Arrange
            Recipe testRecipe = new Recipe("Mac and cheese", "cheese and noodles", "cook it", 5);
            testRecipe.Save();

            //Act
            Recipe foundRecipe = Recipe.Find(testRecipe.GetId());

            //Assert
            Assert.Equal(testRecipe, foundRecipe);
        }


        [Fact]
        public void Test_AddCategory_AddsCategoryToRecipe()
        {
          //Arrange
          Recipe testRecipe = new Recipe("Mac and cheese", "cheese and noodles", "cook it", 5);
          testRecipe.Save();

          Category testCategory = new Category("Mexican");
          testCategory.Save();

          //Act
          testRecipe.AddCategory(testCategory);

          List<Category> result = testRecipe.GetCategories();
          List<Category> testList = new List<Category>{testCategory};

          //Assert
          Assert.Equal(testList, result);
      }

      [Fact]
      public void Test_GetCategories_ReturnsAllRecipeCategories()
      {
        //Arrange
        Recipe testRecipe = new Recipe("Mac and cheese", "cheese and noodles", "cook it", 5);
        testRecipe.Save();

        Category testCategory1 = new Category("Mexican");
        testCategory1.Save();

        Category testCategory2 = new Category("Thai");
        testCategory2.Save();

        //Act
        testRecipe.AddCategory(testCategory1);
        List<Category> result = testRecipe.GetCategories();
        List<Category> testList = new List<Category> {testCategory1};

        //Assert
        Assert.Equal(testList, result);
      }

      [Fact]
      public void Test_Delete_DeletesRecipeAssociationsFromDatabase()
      {
        //Arrange
        Category testCategory = new Category("Mexican");
        testCategory.Save();

        Recipe testRecipe = new Recipe("Mac and cheese", "cheese and noodles", "cook it", 5);
        testRecipe.Save();

        //Act
        testRecipe.AddCategory(testCategory);
        testRecipe.Delete();

        List<Recipe> resultCategoriesRecipes = testCategory.GetRecipes();
        List<Recipe> testCategoriesRecipes = new List<Recipe> {};

        //Assert
        Assert.Equal(testCategoriesRecipes, resultCategoriesRecipes);
      }



        public void Dispose()
        {
          Recipe.DeleteAll();
        Category.DeleteAll();
        }
    }
}
