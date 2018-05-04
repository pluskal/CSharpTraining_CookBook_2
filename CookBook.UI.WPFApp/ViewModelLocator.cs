using AutoMapper;
using CookBook.BL.Facades;
using CookBook.BL.Facades.Mappings;
using CookBook.BL.Repository;
using CookBook.DAL;
using CookBook.UI.WPFApp.ViewModels;
using GalaSoft.MvvmLight.Messaging;

namespace CookBook.UI.WPFApp
{
    public class ViewModelLocator
    {
        private readonly IngredientFacade _ingredientFacade;
        private readonly RecipeFacade _recipeFacade;
        private readonly Messenger _messenger;

        public ViewModelLocator()
        {
            var cookBookDbContext = new CookBookDbContext();
            var unitOfWork = new UnitOfWork(cookBookDbContext);
            var ingredientRepository = new IngredientRepository(unitOfWork);
            var recipeRepository = new RecipeRepository(unitOfWork);
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<IngredientMappingProfile>();
                cfg.AddProfile<RecipeMappingProfile>();
            });
            var mapper = new Mapper(configurationProvider);
            _ingredientFacade = new IngredientFacade(ingredientRepository, mapper);
            _recipeFacade = new RecipeFacade(recipeRepository, mapper);
            _messenger = new Messenger();
        }

        public MainViewModel MainViewModel => new MainViewModel();
        public RecipeListViewModel RecipeListViewModel => new RecipeListViewModel(_messenger, _recipeFacade);
        public RecipeDetailViewModel RecipeDetailViewModel=> new RecipeDetailViewModel(_messenger, _recipeFacade);
        public IngredientListViewModel IngredientListViewModel => new IngredientListViewModel(_messenger, _ingredientFacade);
    }
}