using System.Collections.ObjectModel;
using System.Windows.Input;
using Castle.Core.Internal;
using CookBook.BL.Facades;
using CookBook.UI.WPFApp.Adapters;
using CookBook.UI.WPFApp.Messages;
using CookBook.UI.WPFApp.Models;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace CookBook.UI.WPFApp.ViewModels
{
    public class IngredientListViewModel : ViewModelBase
    {
        private readonly IngredientFacadeAdapter _ingredientFacadeAdapter;
        private readonly Messenger _messenger;
        private ObservableCollection<Ingredient> _ingredients;

        public IngredientListViewModel(Messenger messenger, IngredientFacadeAdapter ingredientFacadeAdapter)
        {
            _messenger = messenger;
            _ingredientFacadeAdapter = ingredientFacadeAdapter;
            SelectionChangedCommand = new RelayCommand<Ingredient>(OnSelectionChanged);

            this._messenger.Register<IngredientsChanged>(this,OnIngredientsChanged);
        }

        private void OnIngredientsChanged(IngredientsChanged obj)
        {
            this.ReloadIngredients();
        }

        public ObservableCollection<Ingredient> Ingredients
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

        private void OnSelectionChanged(Ingredient selectedIngredient)
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
            Ingredients = new ObservableCollection<Ingredient>(_ingredientFacadeAdapter.GetList());
        }
    }
}