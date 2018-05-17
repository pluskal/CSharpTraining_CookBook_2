using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CookBook.BL.Facades;
using CookBook.UI.WPFApp.Adapters;
using CookBook.UI.WPFApp.ViewModels;
using GalaSoft.MvvmLight.Messaging;

namespace CookBook.UI.WPFApp
{
    public class WPFAppWindsorInstaller: IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Install(new FacadesWindsorInstaller());

            container.Register(Component.For<RecipeFacadeAdapter>());
            container.Register(Component.For<IngredientFacadeAdapter>());

            container.Register(Component.For<IMessenger, Messenger>());

            container.Register(Component.For<MainViewModel>());
            container.Register(Component.For<IngredientListViewModel>());
            container.Register(Component.For<IngredientDetailViewModel>());
            container.Register(Component.For<RecipeListViewModel>());
            container.Register(Component.For<RecipeDetailViewModel>());
        }
    }
}