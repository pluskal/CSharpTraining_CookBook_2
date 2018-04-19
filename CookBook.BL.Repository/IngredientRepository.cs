using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookBook.DAL;
using CookBook.DAL.Entities;

namespace CookBook.BL.Repository
{
    public class IngredientRepository : IDisposable
    {
        private readonly CookBookDbContext _cookBookDbContext;

        public IngredientRepository(CookBookDbContext cookBookDbContext)
        {
            this._cookBookDbContext = cookBookDbContext;
        }
        public IEnumerable<IngredientEntity> GetAll()
        {
            return this._cookBookDbContext.Ingredients.ToArray();
        }

        public void Dispose()
        {
            this._cookBookDbContext?.Dispose();
        }
    }
}
