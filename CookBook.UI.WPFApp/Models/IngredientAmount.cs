using System;
using CookBook.Shared.Enums;

namespace CookBook.UI.WPFApp.Models
{
    public class IngredientAmount 
    {
        public IngredientAmount()
        {
            
        }
        public IngredientAmount(RecipeDetail recipeDetail, Ingredient ingredient)
        {
            this.RecipeId = recipeDetail.Id;

            this.IngredientId = ingredient.Id;
            this.IngredientName = ingredient.Name;
            this.IngredientDescription = ingredient.Description;
        }

        public Guid RecipeId { get; set; }
        public Guid IngredientId { get; set; }
        public string IngredientName { get; set; }
        public string IngredientDescription { get; set; }
        public Unit Unit { get; set; }
        public int Amount { get; set; }
    }
}