using System.Collections.ObjectModel;
using CookBook.BL.Facades;
using CookBook.BL.Facades.DTOs;

namespace CookBook.UI.WPFApp.ViewModels
{
    public class RecipeListViewModel
    {
        private readonly RecipeFacade _recipeFacade;

        public RecipeListViewModel(RecipeFacade recipeFacade)
        {
            _recipeFacade = recipeFacade;
        }

        public ObservableCollection<RecipeListDTO> Recipes { get; private set; }

        private void OnLoad()
        {
            Recipes = new ObservableCollection<RecipeListDTO>(_recipeFacade.GetList());
        }
    }
}