using System.Collections.ObjectModel;
using System.ComponentModel;
using Castle.Core.Internal;
using CookBook.BL.Facades;
using CookBook.BL.Facades.DTOs;

namespace CookBook.UI.WPFApp.ViewModels
{
    public class RecipeListViewModel : ViewModelBase
    {
        private readonly RecipeFacade _recipeFacade;
        private ObservableCollection<RecipeListDTO> _recipes;

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

        protected override void OnLoad()
        {
            if (Recipes.IsNullOrEmpty())
            {
                Recipes = new ObservableCollection<RecipeListDTO>(_recipeFacade.GetList());
            }
        }
    }
}