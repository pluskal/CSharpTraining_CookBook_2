using CookBook.DAL.Entities;
using Xunit;

namespace CookBook.DAL.Tests
{
    public class CookBookDbContextTests
    {
        public CookBookDbContextTests()
        {
            this._cookBookDbContext = new CookBookDbContext();

            if (this._cookBookDbContext.Database.Exists())
                this._cookBookDbContext.Database.Delete();
        }

        private readonly CookBookDbContext _cookBookDbContext;

        [Fact]
        //Intent_Method_ExpectedState
        public void CreateDatabase_DatabaseCreate_DatabaseCreated()
        {
            //Act
            this._cookBookDbContext.Database.CreateIfNotExists();

            //Assert
            Assert.True(this._cookBookDbContext.Database.Exists());
        }

        [Fact]
        public void InsertIngredient_IngredientsAdd_IngredientAdded()
        {
            //Arrange
            var ingredient = new IngredientEntity() {Name = "Sugar", Description = "Sweet"};

            //Act
            this._cookBookDbContext.Ingredients.Add(ingredient);
            var savedEntities = this._cookBookDbContext.SaveChanges();

            //Assert
            Assert.Equal(1, savedEntities);
        }
    }
}