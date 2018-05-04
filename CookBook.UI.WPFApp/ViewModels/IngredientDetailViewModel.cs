using System;
using System.Collections.Generic;
using System.Linq;
using CookBook.BL.Facades;
using CookBook.BL.Facades.DTOs;
using CookBook.Shared.Enums;
using CookBook.UI.WPFApp.ViewModels.Messages;
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

            _messenger.Register<SelectedIngredientMessage>(this, OnSelectedIngredient);
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