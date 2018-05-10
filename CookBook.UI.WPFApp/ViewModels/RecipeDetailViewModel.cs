using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using CookBook.BL.Facades;
using CookBook.BL.Facades.DTOs;
using CookBook.Shared.Enums;
using CookBook.UI.WPFApp.Messages;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;

namespace CookBook.UI.WPFApp.ViewModels
{
    public class RecipeDetailViewModel : ViewModelBase
    {
        private readonly IngredientFacade _ingredientFacade;
        private readonly IMessenger _messenger;
        private readonly RecipeFacade _recipeFacade;
        private IEnumerable<IngredientDTO> _ingredientList;
        private RecipeDetailDTO _recipeDetailDTO = new RecipeDetailDTO();

        public RecipeDetailViewModel(IMessenger messenger, RecipeFacade recipeFacade, IngredientFacade ingredientFacade)
        {
            _recipeFacade = recipeFacade;
            _ingredientFacade = ingredientFacade;
            _messenger = messenger;

            _messenger.Register<SelectedRecipeMessage>(this, OnSelectedRecipe);

            AddIngredientCommand = new RelayCommand(OnAddIngredient);
            RemoveIngredientCommand = new RelayCommand<IngredientAmountDTO>(OnRemoveIngredient);

            this._messenger.Register<IngredientsChanged>(this,OnIngredientsChanged);
        }

        private void OnIngredientsChanged(IngredientsChanged ingredientsChanged)
        {
            this.ReloadIngredients();
        }

        public ICommand AddIngredientCommand { get; }

        public IEnumerable<FoodType> FoodTypes => Enum.GetValues(typeof(FoodType)).Cast<FoodType>().ToArray();

        public IEnumerable<IngredientDTO> IngredientList
        {
            get => _ingredientList;
            set
            {
                if (Equals(value, _ingredientList)) return;
                _ingredientList = value;
                OnPropertyChanged();
            }
        }

        public RecipeDetailDTO RecipeDetailDTO
        {
            get => _recipeDetailDTO;
            set
            {
                if (Equals(value, _recipeDetailDTO)) return;
                _recipeDetailDTO = value;
                OnPropertyChanged();
            }
        }

        public ICommand RemoveIngredientCommand { get; }

        protected override void OnLoad()
        {
            base.OnLoad();
            ReloadIngredients();
        }

        private void ReloadIngredients()
        {
            IngredientList = _ingredientFacade.GetList();
        }

        private void OnAddIngredient()
        {
        }

        private void OnRemoveIngredient(IngredientAmountDTO ingredientAmountDTO)
        {
            RecipeDetailDTO.Ingredients.Remove(ingredientAmountDTO);
            RecipeDetailDTO = _recipeFacade.Save(RecipeDetailDTO);
        }

        private void OnSelectedRecipe(SelectedRecipeMessage selectedRecipeMessage)
        {
            RecipeDetailDTO = _recipeFacade.GetDetail(selectedRecipeMessage.RecipeId);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _messenger.Unregister<SelectedRecipeMessage>(this, OnSelectedRecipe);
        }
    }
}