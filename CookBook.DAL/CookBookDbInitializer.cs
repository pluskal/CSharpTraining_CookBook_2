﻿using System;
using System.Data.Entity;
using CookBook.DAL.Entities;

namespace CookBook.DAL
{
    public class CookBookDbInitializer : DropCreateDatabaseIfModelChanges<CookBookDbContext>
    {
        protected override void Seed(CookBookDbContext context)
        {
            var ingredient = new IngredientEntity()
            {
                Id = Guid.Empty,
                Name = $"{nameof(IngredientEntity.Name)}-Seed",
                Description = nameof(RecipeEntity.Description),
            };
              var ingredient1 = new IngredientEntity()
            {
                Id = Guid.Parse("60923986-c08b-4798-9658-fd2c98ca8da4"),
                Name = $"{nameof(IngredientEntity.Name)}-Seed",
                Description = nameof(RecipeEntity.Description),
            };
            
            var recipe = new RecipeEntity
            {
                Id = Guid.Empty,
                Name = $"{nameof(RecipeEntity.Name)}-Seed",
                Description = nameof(RecipeEntity.Description),
                Duration = TimeSpan.FromMinutes(323),
                FoodType = FoodType.Other
            };

            context.Ingredients.Add(ingredient);
            context.Ingredients.Add(ingredient1);
            context.Recipes.Add(recipe);

            base.Seed(context);
        }
    }
}