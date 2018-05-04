using System.Collections.ObjectModel;
using System.Windows.Input;
using Castle.Core.Internal;
using CookBook.BL.Facades;
using CookBook.BL.Facades.DTOs;
using CookBook.UI.WPFApp.Messages;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;

namespace CookBook.UI.WPFApp.ViewModels
{
    public class RecipeListViewModel : ViewModelBase
    {
        private readonly IMessenger _messenger;
        private readonly RecipeFacade _recipeFacade;
        private ObservableCollection<RecipeListDTO> _recipes;

        public RecipeListViewModel(IMessenger messenger, RecipeFacade recipeFacade)
        {
            _messenger = messenger;
            _recipeFacade = recipeFacade;

            SelectionChangedCommand = new RelayCommand<RecipeListDTO>(OnSelectionChanged);
        }

        public ICommand SelectionChangedCommand { get; }

        private void OnSelectionChanged(RecipeListDTO selectedRecipe)
        {
            if (selectedRecipe == null) return;
            _messenger.Send(new SelectedRecipeMessage {RecipeId = selectedRecipe.Id});
        }

        public ObservableCollection<RecipeListDTO> Recipes
        {
            get => _recipes;
            private set
            {
                if (Equals(value, _recipes)) return;
                _recipes = value;
                OnPropertyChanged();
            }
        }

        protected override void OnLoad()
        {
            if (Recipes.IsNullOrEmpty()) Recipes = new ObservableCollection<RecipeListDTO>(_recipeFacade.GetList());
        }
    }
}