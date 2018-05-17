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
            
            _container.Install(new WPFAppWindsorInstaller());

            this.MainWindow = new MainWindow
            {
                DataContext = this._container.Resolve<MainViewModel>()
            };
            this.MainWindow.Show();
        }

        private void App_OnExit(object sender, ExitEventArgs e)
        {
            this._container.Dispose();
        }
    }
}
