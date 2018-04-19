using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookBook.DAL.Entities;

namespace CookBook.DAL
{
    public class CookBookDbContext : DbContext
    {
        public IDbSet<RecipeEntity> Recipes { get; set; }
        public IDbSet<IngredientEntity> Ingredients { get; set; }

        public CookBookDbContext()
        {
            Database.SetInitializer<CookBookDbContext>(new CookBookDbInitializer());
        }
    }

    public class CookBookDbInitializer : DropCreateDatabaseIfModelChanges<CookBookDbContext>
    {
        protected override void Seed(CookBookDbContext context)
        {
            var ingredient = new IngredientEntity()
            {
                Name = $"{nameof(IngredientEntity.Name)}-Seed",
                Description = nameof(RecipeEntity.Description),
            };
            
            var recipe = new RecipeEntity
            {
                Name = $"{nameof(RecipeEntity.Name)}-Seed",
                Description = nameof(RecipeEntity.Description),
                Duration = TimeSpan.FromMinutes(323),
                FoodType = FoodType.Other
            };

            context.Ingredients.Add(ingredient);
            context.Recipes.Add(recipe);

            base.Seed(context);
        }
    }
}
