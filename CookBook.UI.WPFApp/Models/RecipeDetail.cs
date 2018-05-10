using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CookBook.Shared.Enums;
using CookBook.Shared.Interfaces;

namespace CookBook.UI.WPFApp.Models
{
    public class RecipeDetail : IId
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public TimeSpan Duration { get; set; }
        public FoodType FoodType { get; set; }
        public string Description { get; set; }
        public ObservableCollection<IngredientAmount> Ingredients { get; set; } = new ObservableCollection<IngredientAmount>();
    }
}