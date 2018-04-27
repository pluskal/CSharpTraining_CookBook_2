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
        public virtual IDbSet<RecipeEntity> Recipes { get; set; }
        public virtual IDbSet<IngredientEntity> Ingredients { get; set; }

        public CookBookDbContext()
        {
            Database.SetInitializer<CookBookDbContext>(new CookBookDbInitializer());
        }
    }
}
