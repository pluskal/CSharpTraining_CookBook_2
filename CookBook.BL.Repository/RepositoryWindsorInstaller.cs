using System.Data.Entity;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CookBook.DAL;

namespace CookBook.BL.Repository
{
    public class RepositoryWindsorInstaller: IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Install(new DalWindsorInstaller());

            container.Register(Component.For<UnitOfWork>());
            container.Register(Component.For<IngredientRepository>());
            container.Register(Component.For<RecipeRepository>());
        }
    }
}