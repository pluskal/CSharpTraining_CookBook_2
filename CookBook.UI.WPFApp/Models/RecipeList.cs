using System;
using CookBook.Shared.Enums;
using CookBook.Shared.Interfaces;

namespace CookBook.UI.WPFApp.Models
{
    public class RecipeList : IId
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public TimeSpan Duration { get; set; }
        public FoodType FoodType { get; set; }
    }
}