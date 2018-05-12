using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using CookBook.Shared.Enums;
using CookBook.Shared.Interfaces;
using CookBook.UI.WPFApp.Validation;

namespace CookBook.UI.WPFApp.Models
{
    public class RecipeDetail : ValidableModelBase, IId
    {
        public RecipeDetail()
        {
            this.Ingredients = new ObservableCollection<IngredientAmount>();
        }
        public Guid Id
        {
            get => this.GetValue(() => this.Id);
            set => this.SetValue(() => this.Id, value);
        }

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(50, ErrorMessage = "Name exceeded 50 letters")]
        [ExcludeChar("/.,!@#$%", ErrorMessage = "Name contains invalid letters")]
        public string Name
        {
            get => this.GetValue(() => this.Name);
            set => this.SetValue(() => this.Name,value);
        }

        [Required(ErrorMessage = "Duration is required")]
        public TimeSpan Duration
        {
            get => this.GetValue(() => this.Duration);
            set => this.SetValue(() => this.Duration, value);
        }

        [Required(ErrorMessage = "FoodType is required")]
        public FoodType FoodType
        {
            get => this.GetValue(() => this.FoodType);
            set => this.SetValue(() => this.FoodType, value);
        }

        [Required(ErrorMessage = "Description is required")]
        public string Description
        {
            get => this.GetValue(() => this.Description);
            set => this.SetValue(() => this.Description, value);
        }
        public ObservableCollection<IngredientAmount> Ingredients
        {
            get => this.GetValue(() => this.Ingredients);
            set => this.SetValue(() => this.Ingredients, value);
        } 
        public void AddIngredient(IngredientAmount ingredientAmount)
        {
           this.Ingredients.Add(ingredientAmount);
        }

        public void RemoveIngredient(IngredientAmount ingredientAmount)
        {
            this.Ingredients.Remove(ingredientAmount);
        }
    }
}