using System;
using CookBook.DAL.Entities.Base;
using CookBook.Shared.Enums;
using CookBook.Shared.Interfaces;

namespace CookBook.BL.Facades.DTOs
{
    public class IngredientAmountDTO 
    {
        public Guid RecipeId { get; set; }
        public Guid IngredientId { get; set; }
        public string IngredientName { get; set; }
        public string IngredientDescription { get; set; }
        public Unit Unit { get; set; }
        public int Amount { get; set; }
    }
}