using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CookBook.UI.WPFApp.Annotations;
using Microsoft.Expression.Interactivity.Core;

namespace CookBook.UI.WPFApp.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged, IDisposable
    {
        protected ViewModelBase()
        {
            LoadedCommand = new ActionCommand(OnLoad);
        }

        public ICommand LoadedCommand { get; }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnLoad()
        {
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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