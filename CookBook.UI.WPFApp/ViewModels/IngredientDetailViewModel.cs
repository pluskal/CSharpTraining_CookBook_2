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
    public class IngredientDetailViewModel : ViewModelBase
    {
        private readonly Messenger _messenger;
        private readonly IngredientFacade _ingredientFacade;
        private IngredientDTO _ingredientDTO;

        public IngredientDetailViewModel(Messenger messenger, IngredientFacade ingredientFacade)
        {
            _messenger = messenger;
            _ingredientFacade = ingredientFacade;

            this.IngredientDTO = this._ingredientFacade.InitializeNew();

            _messenger.Register<SelectedIngredientMessage>(this, OnSelectedIngredient);

            this.NewCommand = new RelayCommand(OnNew);
            this.SaveCommand = new RelayCommand(OnSave);
            this.DeleteCommand = new RelayCommand(OnDelete);
        }

        private void OnDelete()
        {
            _ingredientFacade.Delete(this.IngredientDTO.Id);
            this.OnNew();
            OnIngredientsChanged();
        }

        private void OnSave()
        {
            this.IngredientDTO = _ingredientFacade.Save(this.IngredientDTO);
            OnIngredientsChanged();
        }

        private void OnIngredientsChanged()
        {
            this._messenger.Send<IngredientsChanged>(new IngredientsChanged());
        }

        private void OnNew()
        {
            this.IngredientDTO = this._ingredientFacade.InitializeNew();
        }

        public IngredientDTO IngredientDTO
        {
            get => _ingredientDTO;
            private set
            {
                if (Equals(value, _ingredientDTO)) return;
                _ingredientDTO = value;
                OnPropertyChanged();
            }
        }
        public IEnumerable<Unit> Units => Enum.GetValues(typeof(Unit)).Cast<Unit>().ToArray();

        public ICommand NewCommand { get; }

        public ICommand SaveCommand { get; }

        public ICommand DeleteCommand { get; }

        private void OnSelectedIngredient(SelectedIngredientMessage selectedIngredientMessage)
        {
           IngredientDTO = this._ingredientFacade.GetDetail(selectedIngredientMessage.IngredientId);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _messenger.Unregister<SelectedIngredientMessage>(this, OnSelectedIngredient);
        }
    }
}