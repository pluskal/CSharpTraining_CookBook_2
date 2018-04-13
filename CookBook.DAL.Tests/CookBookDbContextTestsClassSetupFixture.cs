using System;

namespace CookBook.DAL.Tests
{
    public class CookBookDbContextTestsClassSetupFixture : IDisposable
    {
        public CookBookDbContextTestsClassSetupFixture()
        {
            this.CreateDbContext();
        }

        public CookBookDbContext CookBookDbContext { get; set; }

        public void CreateDbContext()
        {
            this.CookBookDbContext = new CookBookDbContext();

            if (this.CookBookDbContext.Database.Exists())
            {
                this.CookBookDbContext.Database.Delete();
            }
        }

        public void Dispose()
        {
            this.CookBookDbContext?.Dispose();
        }
    }
}