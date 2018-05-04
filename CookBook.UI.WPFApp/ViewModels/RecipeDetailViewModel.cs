using System;
using System.Collections.Generic;
using System.Linq;
using CookBook.BL.Facades;
using CookBook.BL.Facades.DTOs;
using CookBook.Shared.Enums;
using CookBook.UI.WPFApp.Messages;
using GalaSoft.MvvmLight.Messaging;

namespace CookBook.UI.WPFApp.ViewModels
{
    public class RecipeDetailViewModel : ViewModelBase
    {
        private readonly RecipeFacade _recipeFacade;
        private readonly IMessenger _messenger;
        private RecipeDetailDTO _recipeDetailDTO = new RecipeDetailDTO(){};

        public RecipeDetailViewModel(IMessenger messenger, RecipeFacade recipeFacade)
        {
            _recipeFacade = recipeFacade;
            _messenger = messenger;

            _messenger.Register<SelectedRecipeMessage>(this, OnSelectedRecipe);
        }

        private void OnSelectedRecipe(SelectedRecipeMessage selectedRecipeMessage)
        {
            RecipeDetailDTO = this._recipeFacade.GetDetail(selectedRecipeMessage.RecipeId);
        }

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

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _messenger.Unregister<SelectedRecipeMessage>(this, OnSelectedRecipe);
        }
    }
}