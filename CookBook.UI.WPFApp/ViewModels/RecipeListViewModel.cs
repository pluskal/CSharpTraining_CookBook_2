using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Castle.Core.Internal;
using CookBook.BL.Facades;
using CookBook.BL.Facades.DTOs;
using Microsoft.Expression.Interactivity.Core;

namespace CookBook.UI.WPFApp.ViewModels
{
    public class RecipeListViewModel : ViewModelBase
    {
        private readonly RecipeFacade _recipeFacade;
        private ObservableCollection<RecipeListDTO> _recipes;
        private RecipeListDTO _selectedRecipe;

        public RecipeListViewModel(RecipeFacade recipeFacade)
        {
            _recipeFacade = recipeFacade;
        }

        public ObservableCollection<RecipeListDTO> Recipes
        {
            get => _recipes;
            private set
            {
                if (Equals(value, _recipes)) return;
                _recipes = value;
                OnPropertyChanged();
            }
        }

        public RecipeListDTO SelectedRecipe
        {
            get => _selectedRecipe;
            set
            {
                if (Equals(value, _selectedRecipe)) return;
                _selectedRecipe = value;
                OnPropertyChanged();
            }
        }

        protected override void OnLoad()
        {
            if (Recipes.IsNullOrEmpty())
            {
                Recipes = new ObservableCollection<RecipeListDTO>(_recipeFacade.GetList());
            }
        }
    }
}