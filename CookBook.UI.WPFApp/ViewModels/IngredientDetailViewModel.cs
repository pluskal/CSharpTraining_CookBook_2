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
    public class IngredientDetailViewModel : ViewModelBase
    {
        private readonly Messenger _messenger;
        private readonly IngredientFacadeAdapter _ingredientFacadeAdapter;
        private Ingredient _ingredient;

        public IngredientDetailViewModel(Messenger messenger, IngredientFacadeAdapter ingredientFacadeAdapter)
        {
            _messenger = messenger;
            _ingredientFacadeAdapter = ingredientFacadeAdapter;

            this.Ingredient = this._ingredientFacadeAdapter.InitializeNew();

            _messenger.Register<SelectedIngredientMessage>(this, OnSelectedIngredient);

            this.NewCommand = new RelayCommand(OnNew);
            this.SaveCommand = new RelayCommand(OnSave);
            this.DeleteCommand = new RelayCommand(OnDelete);
        }

        private void OnDelete()
        {
            _ingredientFacadeAdapter.Delete(this.Ingredient.Id);
            this.OnNew();
            OnIngredientsChanged();
        }

        private void OnSave()
        {
            this.Ingredient = _ingredientFacadeAdapter.Save(this.Ingredient);
            OnIngredientsChanged();
        }

        private void OnIngredientsChanged()
        {
            this._messenger.Send<IngredientsChanged>(new IngredientsChanged());
        }

        private void OnNew()
        {
            this.Ingredient = this._ingredientFacadeAdapter.InitializeNew();
        }

        public Ingredient Ingredient
        {
            get => _ingredient;
            private set
            {
                if (Equals(value, _ingredient)) return;
                _ingredient = value;
                OnPropertyChanged();
            }
        }
        public IEnumerable<Unit> Units => Enum.GetValues(typeof(Unit)).Cast<Unit>().ToArray();

        public ICommand NewCommand { get; }

        public ICommand SaveCommand { get; }

        public ICommand DeleteCommand { get; }

        private void OnSelectedIngredient(SelectedIngredientMessage selectedIngredientMessage)
        {
           Ingredient = this._ingredientFacadeAdapter.GetDetail(selectedIngredientMessage.IngredientId);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _messenger.Unregister<SelectedIngredientMessage>(this, OnSelectedIngredient);
        }
    }
}