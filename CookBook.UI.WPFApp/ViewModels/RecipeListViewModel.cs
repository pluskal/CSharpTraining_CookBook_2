using System.Collections.ObjectModel;
using System.Windows.Input;
using Castle.Core.Internal;
using CookBook.UI.WPFApp.Adapters;
using CookBook.UI.WPFApp.Messages;
using CookBook.UI.WPFApp.Models;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;

namespace CookBook.UI.WPFApp.ViewModels
{
    public class RecipeListViewModel : ViewModelBase
    {
        private readonly IMessenger _messenger;
        private readonly RecipeFacadeAdapter _recipeFacadeAdapter;
        private ObservableCollection<RecipeList> _recipes;

        public RecipeListViewModel(IMessenger messenger, RecipeFacadeAdapter recipeFacadeAdapter)
        {
            _messenger = messenger;
            _recipeFacadeAdapter = recipeFacadeAdapter;

            SelectionChangedCommand = new RelayCommand<RecipeList>(OnSelectionChanged);

            _messenger.Register<RecipeChanged>(this, OnRecipeChanged);
        }

        public ObservableCollection<RecipeList> Recipes
        {
            get => _recipes;
            private set
            {
                if (Equals(value, _recipes)) return;
                _recipes = value;
                OnPropertyChanged();
            }
        }

        public ICommand SelectionChangedCommand { get; }

        private void OnRecipeChanged(RecipeChanged obj)
        {
            ReloadRecipes();
        }

        private void OnSelectionChanged(RecipeList selectedRecipe)
        {
            if (selectedRecipe == null) return;
            _messenger.Send(new SelectedRecipeMessage {RecipeId = selectedRecipe.Id});
        }

        protected override void OnLoad()
        {
            if (Recipes.IsNullOrEmpty()) ReloadRecipes();
        }

        private void ReloadRecipes()
        {
            Recipes = new ObservableCollection<RecipeList>(_recipeFacadeAdapter.GetList());
        }
    }
}