using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CookBook.Shared.Enums;
using CookBook.Shared.Interfaces;

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
        public string Name
        {
            get => this.GetValue(() => this.Name);
            set => this.SetValue(() => this.Name,value);
        }
        public TimeSpan Duration
        {
            get => this.GetValue(() => this.Duration);
            set => this.SetValue(() => this.Duration, value);
        }
        public FoodType FoodType
        {
            get => this.GetValue(() => this.FoodType);
            set => this.SetValue(() => this.FoodType, value);
        }
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