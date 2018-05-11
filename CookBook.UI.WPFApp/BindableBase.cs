using System.ComponentModel;
using System.Runtime.CompilerServices;
using CookBook.UI.WPFApp.Annotations;

namespace CookBook.UI.WPFApp
{
    public class BindableBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}