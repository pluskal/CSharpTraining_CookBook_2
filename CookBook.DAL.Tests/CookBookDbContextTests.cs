using System;
using System.Collections.Generic;
using System.Linq;
using CookBook.DAL.Entities;
using Xunit;

namespace CookBook.DAL.Tests
{
    public class CookBookDbContextTests : IClassFixture<CookBookDbContextTestsClassSetupFixture>
    {
        public CookBookDbContextTests(CookBookDbContextTestsClassSetupFixture cookBookDbContextTestsClassSetupFixture)
        {
            this._cookBookDbContextTestsClassSetupFixture = cookBookDbContextTestsClassSetupFixture;
        }

        private readonly CookBookDbContextTestsClassSetupFixture _cookBookDbContextTestsClassSetupFixture;

        private CookBookDbContext CookBookDbContext => this._cookBookDbContextTestsClassSetupFixture.CookBookDbContext;

        private static IngredientEntity CreateSugarIngredient()
        {
            return new IngredientEntity {Name = "Sugar", Description = "Sweet"};
        }

        private static RecipeEntity CreateEmptyRecipe()
        {
            return new RecipeEntity
            {
                Name = nameof(RecipeEntity.Name),
                Description = nameof(RecipeEntity.Description),
                Duration = TimeSpan.FromMinutes(323),
                FoodType = FoodType.Other
            };
        }

        private static RecipeEntity CreateLemonadeRecipe()
        =>
             new RecipeEntity
            {
                Name = "Lemonade",
                Description = "Sweet summer drink",
                Duration = TimeSpan.FromMinutes(2),
                FoodType = FoodType.Other,
                Ingredients = new List<IngredientAmountEntity>
                {
                    new IngredientAmountEntity
                    {
                        Amount = 1,
                        Unit = Unit.L,
                        Ingredient = new IngredientEntity
                        {
                            Name = "Water",
                            Description = "Sprinkling"
                        }
                    },
                    new IngredientAmountEntity
                    {
                        Amount = 1,
                        Unit = Unit.L,
                        Ingredient = new IngredientEntity
                        {
                            Name = "Lemon",
                            Description = "Sweet lemon"
                        }
                    }
                }
            };
        


        [Fact(Skip = "")]
        //Intent_Method_ExpectedState
        public void CreateDatabase_DatabaseCreate_DatabaseCreated()
        {
            //Act
            this.CookBookDbContext.Database.CreateIfNotExists();

            //Assert
            Assert.True(this.CookBookDbContext.Database.Exists());
        }

        [Fact(Skip = "")]
        public void GetIngredient_IngredientsFirst_IngredientObtained()
        {
            //Arrange
            //Act
            var ingredient = this.CookBookDbContext.Ingredients.FirstOrDefault();

            //Assert
            Assert.NotNull(ingredient);
        }

        [Fact(Skip = "")]
        public void GetRecipe_RecipesFirst_RecipeObtained()
        {
            //Arrange
            //Act
            var recipe = this.CookBookDbContext.Recipes.FirstOrDefault();

            //Assert
            Assert.NotNull(recipe);
        }

        [Fact]
        public void InsertEmptyRecipe_RecipesAdd_RecipeAdded()
        {
            //Arrange
            var recipe = CreateEmptyRecipe();

            //Act
            this.CookBookDbContext.Recipes.Add(recipe);
            var savedEntities = this.CookBookDbContext.SaveChanges();

            //Assert
            Assert.Equal(1, savedEntities);
        }


        [Fact]
        public void InsertGetLemonadeRecipe_RecipesAdd_RecipesGet_RecipeAddedAndObtained()
        {
            //Arrange
            var lemonadeRecipe = CreateLemonadeRecipe();

            //Act
            this.CookBookDbContext.Recipes.Add(lemonadeRecipe);
            var savedEntities = this.CookBookDbContext.SaveChanges();

            var lemonadeRecipeFromDb = this.CookBookDbContext.Recipes.First(r => r.Id == lemonadeRecipe.Id);

            //Assert
            Assert.Equal(9, savedEntities);
            Assert.NotNull(lemonadeRecipeFromDb);
            Assert.Equal(lemonadeRecipe, lemonadeRecipeFromDb);
        }

        [Fact]
        public void InsertIngredient_IngredientsAdd_IngredientAdded()
        {
            //Arrange
            var ingredient = CreateSugarIngredient();

            //Act
            this.CookBookDbContext.Ingredients.Add(ingredient);
            var savedEntities = this.CookBookDbContext.SaveChanges();

            //Assert
            Assert.Equal(1, savedEntities);
        }

        [Fact]
        public void InsertAndGetIngredient_IngredientsAdd_IngredientAddedAndObtained()
        {
            //Arrange
            var ingredient = CreateSugarIngredient();

            //Act
            this.CookBookDbContext.Ingredients.Add(ingredient);
            var savedEntities = this.CookBookDbContext.SaveChanges();

            IngredientEntity ingredientFromDb;
            using (var dbx = new CookBookDbContext())
            {
                ingredientFromDb = dbx.Ingredients.First(i => i.Id == ingredient.Id);
            }

            //Assert
            Assert.Equal(1, savedEntities);

            Assert.Equal(ingredient.Id, ingredientFromDb.Id);
            Assert.Equal(ingredient, ingredientFromDb);
        }
    }
}