namespace CookBook.UI.WPFApp.ViewModels
{
    public class MainViewModel
    {
        public MainViewModel(
            IngredientListViewModel ingredientListViewModel,
            IngredientDetailViewModel ingredientDetailViewModel, 
            RecipeDetailViewModel recipeDetailViewModel,
            RecipeListViewModel recipeListViewModel)
        {
            IngredientListViewModel = ingredientListViewModel;
            IngredientDetailViewModel = ingredientDetailViewModel;
            RecipeDetailViewModel = recipeDetailViewModel;
            RecipeListViewModel = recipeListViewModel;
        }

        public IngredientDetailViewModel IngredientDetailViewModel { get; }
        public IngredientListViewModel IngredientListViewModel { get; }
        public RecipeDetailViewModel RecipeDetailViewModel { get; }
        public RecipeListViewModel RecipeListViewModel { get; }
    }
}