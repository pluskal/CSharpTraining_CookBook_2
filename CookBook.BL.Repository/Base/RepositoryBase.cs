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

        public void Insert(TEntity entity)
        {
            entity.Id = Guid.NewGuid();
            UnitOfWork.Context.Set<TEntity>().Add(entity);
        }

        public void Delete(TEntity entity)
        {
            UnitOfWork.Context.Set<TEntity>().Remove(entity);
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

        public void Update(TEntity entity)
        {
            this.Delete(entity.Id);
            this.Insert(entity);
            //UnitOfWork.Context.Entry(entity).State = EntityState.Modified;
        }

        public TEntity InitializeNew()
        {
            return new TEntity {Id = Guid.Empty};
        }
    }
}