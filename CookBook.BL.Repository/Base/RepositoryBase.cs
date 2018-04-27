using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CookBook.DAL.Entities.Base;

namespace CookBook.BL.Repository.Base
{
    public class RepositoryBase<TEntity> : IDisposable where TEntity : EntityBase, new()
    {
        public RepositoryBase(UnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public UnitOfWork UnitOfWork { get; }

        public void Dispose()
        {
            UnitOfWork.Context?.Dispose();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return UnitOfWork.Context.Set<TEntity>().ToArray();
        }

        public TEntity GetById(Guid id)
        {
            return UnitOfWork.Context.Set<TEntity>().FirstOrDefault(i => i.Id == id);
        }

        public void Insert(TEntity ingredientEntity)
        {
            ingredientEntity.Id = Guid.NewGuid();
            UnitOfWork.Context.Set<TEntity>().Add(ingredientEntity);
        }

        public void Delete(TEntity ingredientEntity)
        {
            UnitOfWork.Context.Set<TEntity>().Remove(ingredientEntity);
        }

        public void Delete(Guid id)
        {
            var entity = UnitOfWork.Context.Set<TEntity>().Local.SingleOrDefault(e => e.Id.Equals(id));

            if (entity == null)
            {
                entity = new TEntity {Id = id};
                UnitOfWork.Context.Set<TEntity>().Attach(entity);
            }

            Delete(entity);
        }

        public void Update(TEntity ingredientEntity)
        {
            UnitOfWork.Context.Entry(ingredientEntity).State = EntityState.Modified;
        }

        public TEntity InitializeNew()
        {
            return new TEntity {Id = Guid.Empty};
        }
    }
}