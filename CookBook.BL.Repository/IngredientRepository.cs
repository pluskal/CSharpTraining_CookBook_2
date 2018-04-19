using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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

        public IngredientEntity GetById(Guid id)
        {
            return this._cookBookDbContext.Ingredients.FirstOrDefault(i => i.Id == id);
        }

        public void Insert(IngredientEntity ingredientEntity)
        {
            this._cookBookDbContext.Ingredients.Add(ingredientEntity);
            this._cookBookDbContext.SaveChanges();
        }

        public void Delete(IngredientEntity ingredientEntity)
        {
            this._cookBookDbContext.Ingredients.Remove(ingredientEntity);
        }

        public void Delete(Guid id)
        {
            var entity = this._cookBookDbContext.Ingredients.Local.SingleOrDefault(e => e.Id.Equals(id));

            if (entity == null)
            {
                entity = new IngredientEntity { Id = id };
                this._cookBookDbContext.Ingredients.Attach(entity);
            }

            this.Delete(entity);
        }

        public void Update(IngredientEntity ingredientEntity)
        {
            this._cookBookDbContext.Entry(ingredientEntity).State = EntityState.Modified;
            this._cookBookDbContext.SaveChanges();

        }

        public void Dispose()
        {
            this._cookBookDbContext?.Dispose();
        }
    }
}
