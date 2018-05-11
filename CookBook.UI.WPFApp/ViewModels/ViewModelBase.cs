using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CookBook.UI.WPFApp.Annotations;
using Microsoft.Expression.Interactivity.Core;

namespace CookBook.UI.WPFApp.ViewModels
{
    public abstract class ViewModelBase : BindableBase, IDisposable
    {
        protected ViewModelBase()
        {
            LoadedCommand = new ActionCommand(OnLoad);
        }

        public ICommand LoadedCommand { get; }

        protected virtual void OnLoad()
        {
        }
        
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}