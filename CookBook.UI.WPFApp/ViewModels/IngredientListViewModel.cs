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
        private ObservableCollection<IngredientDTO> _ingredients;

        public IngredientListViewModel(Messenger messenger, IngredientFacade ingredientFacade)
        {
            _messenger = messenger;
            _ingredientFacade = ingredientFacade;
            SelectionChangedCommand = new RelayCommand<IngredientDTO>(OnSelectionChanged);

            this._messenger.Register<IngredientsChanged>(this,OnIngredientsChanged);
        }

        private void OnIngredientsChanged(IngredientsChanged obj)
        {
            this.ReloadIngredients();
        }

        public ObservableCollection<IngredientDTO> Ingredients
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

        private void OnSelectionChanged(IngredientDTO selectedIngredient)
        {
            if (selectedIngredient == null) return;
            _messenger.Send(new SelectedIngredientMessage {IngredientId = selectedIngredient.Id});
        }

        protected override void OnLoad()
        {
            if (Ingredients.IsNullOrEmpty())
                ReloadIngredients();
        }

        private void ReloadIngredients()
        {
            Ingredients = new ObservableCollection<IngredientDTO>(_ingredientFacade.GetList());
        }
    }
}