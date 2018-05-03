using CookBook.BL.Facades;
using CookBook.BL.Facades.DTOs;

namespace CookBook.UI.WPFApp.ViewModels
{
    public class RecipeDetailViewModel : ViewModelBase
    {
        private readonly RecipeFacade _recipeFacade;

        public RecipeDetailViewModel(RecipeFacade recipeFacade)
        {
            _recipeFacade = recipeFacade;
        }

        public RecipeDetailDTO RecipeDetailDTO { get; set; } = new RecipeDetailDTO(){};
    }
}