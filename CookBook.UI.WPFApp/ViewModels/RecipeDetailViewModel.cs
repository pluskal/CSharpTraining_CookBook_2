using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using CookBook.BL.Facades;
using CookBook.Shared.Enums;
using CookBook.UI.WPFApp.Adapters;
using CookBook.UI.WPFApp.Messages;
using CookBook.UI.WPFApp.Models;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;

namespace CookBook.UI.WPFApp.ViewModels
{
    public class RecipeDetailViewModel : ViewModelBase
    {
        private readonly IngredientFacadeAdapter _ingredientFacadeAdapter;
        private readonly IMessenger _messenger;
        private readonly RecipeFacadeAdapter _recipeFacadeAdapter;
        private IEnumerable<Ingredient> _ingredientList;
        private RecipeDetail _recipeDetail = new RecipeDetail();

        public RecipeDetailViewModel(IMessenger messenger, RecipeFacadeAdapter recipeFacadeAdapter, IngredientFacadeAdapter ingredientFacadeAdapter)
        {
            _recipeFacadeAdapter = recipeFacadeAdapter;
            _ingredientFacadeAdapter = ingredientFacadeAdapter;
            _messenger = messenger;

            _messenger.Register<SelectedRecipeMessage>(this, OnSelectedRecipe);

            AddIngredientCommand = new RelayCommand<Ingredient>(OnAddIngredient);
            RemoveIngredientCommand = new RelayCommand<IngredientAmount>(OnRemoveIngredient);

            this._messenger.Register<IngredientsChanged>(this,OnIngredientsChanged);
        }

        private void OnIngredientsChanged(IngredientsChanged ingredientsChanged)
        {
            this.ReloadIngredients();
        }

        public ICommand AddIngredientCommand { get; }

        public IEnumerable<FoodType> FoodTypes => Enum.GetValues(typeof(FoodType)).Cast<FoodType>().ToArray();

        public IEnumerable<Ingredient> IngredientList
        {
            get => _ingredientList;
            set
            {
                if (Equals(value, _ingredientList)) return;
                _ingredientList = value;
                OnPropertyChanged();
            }
        }

        public RecipeDetail RecipeDetail
        {
            get => _recipeDetail;
            set
            {
                if (Equals(value, _recipeDetail)) return;
                _recipeDetail = value;
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
            IngredientList = _ingredientFacadeAdapter.GetList();
        }

        private void OnAddIngredient(Ingredient ingredient)
        {
            if (ingredient == null)
            {
                return;
            }
            this.RecipeDetail.Ingredients.Add(new IngredientAmount(this.RecipeDetail, ingredient));
        }

        private void OnRemoveIngredient(IngredientAmount ingredientAmount)
        {
            RecipeDetail.Ingredients.Remove(ingredientAmount);
            RecipeDetail = _recipeFacadeAdapter.Save(RecipeDetail);
        }

        private void OnSelectedRecipe(SelectedRecipeMessage selectedRecipeMessage)
        {
            RecipeDetail = _recipeFacadeAdapter.GetDetail(selectedRecipeMessage.RecipeId);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _messenger.Unregister<SelectedRecipeMessage>(this, OnSelectedRecipe);
        }
    }
}