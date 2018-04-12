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
    }
}
