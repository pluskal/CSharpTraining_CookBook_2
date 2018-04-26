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
        public UnitOfWork UnitOfWork { get; }

        public RepositoryBase(UnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }
        public IEnumerable<TEntity> GetAll()
        {
            return this.UnitOfWork.Context.Set<TEntity>().ToArray();
        }

        public TEntity GetById(Guid id)
        {
            return this.UnitOfWork.Context.Set<TEntity>().FirstOrDefault(i => i.Id == id);
        }

        public void Insert(TEntity ingredientEntity)
        {
            ingredientEntity.Id = Guid.NewGuid();
            this.UnitOfWork.Context.Set<TEntity>().Add(ingredientEntity);
        }

        public void Delete(TEntity ingredientEntity)
        {
            this.UnitOfWork.Context.Set<TEntity>().Remove(ingredientEntity);
        }

        public void Delete(Guid id)
        {
            var entity = this.UnitOfWork.Context.Set<TEntity>().Local.SingleOrDefault(e => e.Id.Equals(id));

            if (entity == null)
            {
                entity = new TEntity { Id = id };
                this.UnitOfWork.Context.Set<TEntity>().Attach(entity);
            }

            this.Delete(entity);
        }

        public void Update(TEntity ingredientEntity)
        {
            this.UnitOfWork.Context.Entry(ingredientEntity).State = EntityState.Modified;
        }

        public TEntity InitializeNew()
        {
            return new TEntity {Id = Guid.Empty};
        }

        public void Dispose()
        {
            this.UnitOfWork.Context?.Dispose();
        }
    }
}