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
        private readonly IMessenger _messenger;
        private readonly RecipeFacade _recipeFacade;
        private RecipeDetailDTO _recipeDetailDTO = new RecipeDetailDTO();

        public RecipeDetailViewModel(IMessenger messenger, RecipeFacade recipeFacade)
        {
            _recipeFacade = recipeFacade;
            _messenger = messenger;

            _messenger.Register<SelectedRecipeMessage>(this, OnSelectedRecipe);

            AddIngredientCommand = new RelayCommand(OnAddIngredient);
            DeleteIngredientCommand = new RelayCommand<IngredientAmountDTO>(OnDeleteIngredient);
        }

        public ICommand AddIngredientCommand { get; }

        public ICommand DeleteIngredientCommand { get; }

        public IEnumerable<FoodType> FoodTypes => Enum.GetValues(typeof(FoodType)).Cast<FoodType>().ToArray();

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

        private void OnAddIngredient()
        {
        }

        private void OnDeleteIngredient(IngredientAmountDTO ingredientAmountDTO)
        {
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