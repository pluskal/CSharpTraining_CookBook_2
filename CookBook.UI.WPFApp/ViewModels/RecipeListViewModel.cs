using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookBook.BL.Facades;
using CookBook.BL.Facades.DTOs;

namespace CookBook.UI.WPFApp.ViewModels
{
    public class RecipeListViewModel
    {
        public RecipeListViewModel(RecipeFacade recipeFacade)
        {
            var recipeListDtos = recipeFacade.GetList();
            this.Recipes = new ObservableCollection<RecipeListDTO>(recipeListDtos);
        }

        public ObservableCollection<RecipeListDTO> Recipes { get; }
    }
}
