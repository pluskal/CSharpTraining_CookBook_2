using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AutoMapper;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using CookBook.BL.Facades;
using CookBook.BL.Facades.Mappings;
using CookBook.BL.Repository;
using CookBook.DAL;
using CookBook.UI.WPFApp.Adapters;
using CookBook.UI.WPFApp.Adapters.Mappings;
using CookBook.UI.WPFApp.ViewModels;
using CookBook.UI.WPFApp.Views;
using GalaSoft.MvvmLight.Messaging;

namespace CookBook.UI.WPFApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private WindsorContainer _container;

        public App()
        {
        }

        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            _container = new WindsorContainer();

            _container.Register(Component.For<DbContext, CookBookDbContext>());
            _container.Register(Component.For<UnitOfWork>());
            _container.Register(Component.For<IngredientRepository>());
            _container.Register(Component.For<RecipeRepository>());

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<IngredientDTOMappingProfile>();
                cfg.AddProfile<RecipeDTOMappingProfile>();
                cfg.AddProfile<RecipeMappingProfile>();
                cfg.AddProfile<IngredientMappingProfile>();
            });
            _container.Register(Component.For<IMapper, Mapper>().Instance(new Mapper(configurationProvider)));
            _container.Register(Component.For<IngredientFacade>());
            _container.Register(Component.For<RecipeFacade>());

            _container.Register(Component.For<RecipeFacadeAdapter>());
            _container.Register(Component.For<IngredientFacadeAdapter>());

            _container.Register(Component.For<IMessenger, Messenger>());

            _container.Register(Component.For<MainViewModel>());
            _container.Register(Component.For<IngredientListViewModel>());
            _container.Register(Component.For<IngredientDetailViewModel>());
            _container.Register(Component.For<RecipeListViewModel>());
            _container.Register(Component.For<RecipeDetailViewModel>());

            this.MainWindow = new MainWindow {DataContext = this._container.Resolve<MainViewModel>()};
            this.MainWindow.Show();
        }
    }
}
