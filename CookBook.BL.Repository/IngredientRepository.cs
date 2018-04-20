using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using CookBook.BL.Repository.Base;
using CookBook.DAL;
using CookBook.DAL.Entities;

namespace CookBook.BL.Repository
{
    public class IngredientRepository : RepositoryBase<IngredientEntity>
    {
        public IngredientRepository(CookBookDbContext cookBookDbContext) : base(cookBookDbContext)
        {
        }
    }
}
