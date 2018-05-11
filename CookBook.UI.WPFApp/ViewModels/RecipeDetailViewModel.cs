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
        private RecipeDetail _recipeDetail;

        public RecipeDetailViewModel(IMessenger messenger, RecipeFacadeAdapter recipeFacadeAdapter, IngredientFacadeAdapter ingredientFacadeAdapter)
        {
            _recipeFacadeAdapter = recipeFacadeAdapter;
            _ingredientFacadeAdapter = ingredientFacadeAdapter;
            _messenger = messenger;

            _messenger.Register<SelectedRecipeMessage>(this, OnSelectedRecipe);

            AddIngredientCommand = new RelayCommand<Ingredient>(OnAddIngredient);
            RemoveIngredientCommand = new RelayCommand<IngredientAmount>(OnRemoveIngredient);

            NewRecipeCommand = new RelayCommand(OnNewRecipe);
            SaveRecipeCommand = new RelayCommand<RecipeDetail>(OnSaveRecipe);
            DeleteRecipeCommand = new RelayCommand<RecipeDetail>(OnDeleteRecipe);

            this._messenger.Register<IngredientsChanged>(this,OnIngredientsChanged);

            InitializeNewRecipe();
        }

        private void OnDeleteRecipe(RecipeDetail recipeDetail)
        {
            if (recipeDetail == null) return;
            this._recipeFacadeAdapter.Delete(recipeDetail);
            this.InitializeNewRecipe();
        }

        private void NotifyRecipeChanged()
        {
            this._messenger.Send(new RecipeChanged());
        }

        private void OnSaveRecipe(RecipeDetail recipeDetail)
        {
            if(recipeDetail == null) return;
            this.RecipeDetail = this._recipeFacadeAdapter.Save(recipeDetail);
        }

        private void OnNewRecipe()
        {
            InitializeNewRecipe();
        }

        private void InitializeNewRecipe()
        {
            this.RecipeDetail = this._recipeFacadeAdapter.InitializeNew();
        }

        public ICommand NewRecipeCommand { get; }

        public ICommand SaveRecipeCommand { get; }

        public ICommand DeleteRecipeCommand { get; }

        private void OnIngredientsChanged(IngredientsChanged ingredientsChanged)
        {
            this.ReloadIngredients();
        }

        public ICommand AddIngredientCommand { get; }

        public IEnumerable<FoodType> FoodTypes => Enum.GetValues(typeof(FoodType)).Cast<FoodType>().ToArray();
        public IEnumerable<Unit> Units => Enum.GetValues(typeof(Unit)).Cast<Unit>().ToArray();

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
            get => this.GetValue(() => this.RecipeDetail);
            set
            {
                this.SetValue(() => this.RecipeDetail,value);
                NotifyRecipeChanged();
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
            this.RecipeDetail.AddIngredient(new IngredientAmount(this.RecipeDetail, ingredient));
        }

        private void OnRemoveIngredient(IngredientAmount ingredientAmount)
        {
            RecipeDetail.RemoveIngredient(ingredientAmount);
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