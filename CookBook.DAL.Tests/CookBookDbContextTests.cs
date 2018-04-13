using CookBook.DAL.Entities;
using Xunit;

namespace CookBook.DAL.Tests
{
    public class CookBookDbContextTests : IClassFixture<CookBookDbContextTestsClassSetupFixture>
    {
        private readonly CookBookDbContextTestsClassSetupFixture _cookBookDbContextTestsClassSetupFixture;

        public CookBookDbContextTests(CookBookDbContextTestsClassSetupFixture cookBookDbContextTestsClassSetupFixture)
        {
            this._cookBookDbContextTestsClassSetupFixture = cookBookDbContextTestsClassSetupFixture;
        }

        private CookBookDbContext CookBookDbContext => this._cookBookDbContextTestsClassSetupFixture.CookBookDbContext;

        [Fact]
        //Intent_Method_ExpectedState
        public void CreateDatabase_DatabaseCreate_DatabaseCreated()
        {
            //Act
            this.CookBookDbContext.Database.CreateIfNotExists();

            //Assert
            Assert.True(this.CookBookDbContext.Database.Exists());
        }

        [Fact]
        public void InsertIngredient_IngredientsAdd_IngredientAdded()
        {
            //Arrange
            var ingredient = new IngredientEntity() {Name = "Sugar", Description = "Sweet"};

            //Act
            this.CookBookDbContext.Ingredients.Add(ingredient);
            var savedEntities = this.CookBookDbContext.SaveChanges();

            //Assert
            Assert.Equal(1, savedEntities);
        }
    }
}