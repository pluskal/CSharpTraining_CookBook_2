using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookBook.BL.Repository
{
    public class UnitOfWork:IDisposable
    {
        public DbContext Context { get; }

        public UnitOfWork(DbContext dbContext)
        {
            this.Context = dbContext;
        }

        public void Commit()
        {
            this.Context.SaveChanges();
        }

        public void Dispose()
        {
            this.Context?.Dispose();
        }
    }
}
