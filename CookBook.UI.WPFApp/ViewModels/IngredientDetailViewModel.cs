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
        private IngredientDTO _ingredientDetailDTO;

        public IngredientDetailViewModel(Messenger messenger, IngredientFacade ingredientFacade)
        {
            _messenger = messenger;
            _ingredientFacade = ingredientFacade;

            _messenger.Register<SelectedIngredientMessage>(this, OnSelectedIngredient);
        }

        public IngredientDTO IngredientDetailDTO
        {
            get => _ingredientDetailDTO;
            private set
            {
                if (Equals(value, _ingredientDetailDTO)) return;
                _ingredientDetailDTO = value;
                OnPropertyChanged();
            }
        }
        public IEnumerable<Unit> Units => Enum.GetValues(typeof(Unit)).Cast<Unit>().ToArray();
        
        private void OnSelectedIngredient(SelectedIngredientMessage selectedIngredientMessage)
        {
           IngredientDetailDTO = this._ingredientFacade.GetDetail(selectedIngredientMessage.IngredientId);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _messenger.Unregister<SelectedIngredientMessage>(this, OnSelectedIngredient);
        }
    }
}