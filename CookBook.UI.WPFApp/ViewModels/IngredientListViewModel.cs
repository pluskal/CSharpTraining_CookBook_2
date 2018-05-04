using System.Collections.ObjectModel;
using System.Windows.Input;
using Castle.Core.Internal;
using CookBook.BL.Facades;
using CookBook.BL.Facades.DTOs;
using CookBook.UI.WPFApp.ViewModels.Messages;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace CookBook.UI.WPFApp.ViewModels
{
    public class IngredientListViewModel : ViewModelBase
    {
        private readonly IngredientFacade _ingredientFacade;
        private readonly Messenger _messenger;
        private ObservableCollection<IngredientListDTO> _ingredients;

        public IngredientListViewModel(Messenger messenger, IngredientFacade ingredientFacade)
        {
            _messenger = messenger;
            _ingredientFacade = ingredientFacade;
            SelectionChangedCommand = new RelayCommand<IngredientListDTO>(OnSelectionChanged);
        }

        public ObservableCollection<IngredientListDTO> Ingredients
        {
            get => _ingredients;
            set
            {
                if (Equals(value, _ingredients)) return;
                _ingredients = value;
                OnPropertyChanged();
            }
        }

        public ICommand SelectionChangedCommand { get; }

        private void OnSelectionChanged(IngredientListDTO selectedIngredient)
        {
            if (selectedIngredient == null) return;
            _messenger.Send(new SelectedIngredientMessage {IngredientId = selectedIngredient.Id});
        }

        protected override void OnLoad()
        {
            if (Ingredients.IsNullOrEmpty())
                Ingredients = new ObservableCollection<IngredientListDTO>(_ingredientFacade.GetList());
        }
    }
}