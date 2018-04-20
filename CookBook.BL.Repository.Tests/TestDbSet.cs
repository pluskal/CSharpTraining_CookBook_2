using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using CookBook.DAL.Entities.Base;

namespace CookBook.BL.Repository.Tests
{
    /// <summary>
    /// Based on https://github.com/riganti/infrastructure/blob/master/src/Infrastructure/Tests/Riganti.Utils.Infrastructure.EntityFramework.Tests/Repository/DbContextMockExtensions.cs
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class TestDbSet<TEntity> : DbSet<TEntity>, IDbSet<TEntity>
        where TEntity : EntityBase, new()
    {
        public override ObservableCollection<TEntity> Local { get; }
        private IQueryable LocalQueryable => this.Local.AsQueryable();

        public TestDbSet() : this(new List<TEntity>())
        {
        }

        public TestDbSet(IEnumerable<TEntity> data)
        {
            this.Local = new ObservableCollection<TEntity>(data);
        }

        public override TEntity Find(params object[] keyValues)
        {
            throw new NotImplementedException("Derive from TestDbSet<T> and override Find");
        }

        public override TEntity Add(TEntity item)
        {
            this.Local.Add(item);
            return item;
        }

        public override TEntity Remove(TEntity item)
        {
            var wasRemoved = this.Local.Remove(item);
            if (!wasRemoved)
            {
                var itemWithSameId = this.Local.SingleOrDefault(i => Equals(i.Id, item.Id));
                if (itemWithSameId != null)
                {
                    this.Local.Remove(itemWithSameId);
                }
            }
            return item;
        }

        public override TEntity Attach(TEntity item)
        {
            var itemWithSameId = this.Local.SingleOrDefault(i => Equals(i.Id, item.Id));
            if (itemWithSameId == null)
            {
                this.Local.Add(item);
            }
            return item;
        }

        public override TEntity Create()
        {
            return new TEntity();
        }

        public override TDerivedEntity Create<TDerivedEntity>()
        {
            return Activator.CreateInstance<TDerivedEntity>();
        }

        Type IQueryable.ElementType => this.LocalQueryable.ElementType;

        Expression IQueryable.Expression => this.LocalQueryable.Expression;

        IQueryProvider IQueryable.Provider => this.LocalQueryable.Provider;

        IEnumerator IEnumerable.GetEnumerator() => this.Local.GetEnumerator();

        IEnumerator<TEntity> IEnumerable<TEntity>.GetEnumerator() => this.Local.GetEnumerator();
    }
}