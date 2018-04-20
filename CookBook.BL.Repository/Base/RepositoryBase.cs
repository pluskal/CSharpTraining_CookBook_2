using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CookBook.DAL;
using CookBook.DAL.Entities.Base;

namespace CookBook.BL.Repository.Base
{
    public class RepositoryBase<TEntity> : IDisposable where TEntity : EntityBase, new()
    {
        private readonly CookBookDbContext _cookBookDbContext;

        public RepositoryBase(CookBookDbContext cookBookDbContext)
        {
            this._cookBookDbContext = cookBookDbContext;
        }
        public IEnumerable<TEntity> GetAll()
        {
            return this._cookBookDbContext.Set<TEntity>().ToArray();
        }

        public TEntity GetById(Guid id)
        {
            return this._cookBookDbContext.Set<TEntity>().FirstOrDefault(i => i.Id == id);
        }

        public void Insert(TEntity ingredientEntity)
        {
            this._cookBookDbContext.Set<TEntity>().Add(ingredientEntity);
            this._cookBookDbContext.SaveChanges();
        }

        public void Delete(TEntity ingredientEntity)
        {
            this._cookBookDbContext.Set<TEntity>().Remove(ingredientEntity);
            this._cookBookDbContext.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var entity = this._cookBookDbContext.Set<TEntity>().Local.SingleOrDefault(e => e.Id.Equals(id));

            if (entity == null)
            {
                entity = new TEntity { Id = id };
                this._cookBookDbContext.Set<TEntity>().Attach(entity);
            }

            this.Delete(entity);
        }

        public void Update(TEntity ingredientEntity)
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