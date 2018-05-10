using System;
using CookBook.Shared.Enums;

namespace CookBook.UI.WPFApp.Models
{
    public class IngredientAmount 
    {
        public Guid RecipeId { get; set; }
        public Guid IngredientId { get; set; }
        public string IngredientName { get; set; }
        public string IngredientDescription { get; set; }
        public Unit Unit { get; set; }
        public int Amount { get; set; }
    }
}