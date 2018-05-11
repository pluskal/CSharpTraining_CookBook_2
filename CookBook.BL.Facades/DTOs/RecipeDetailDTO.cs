using System;
using System.Collections.Generic;
using CookBook.DAL.Entities;
using CookBook.Shared.Enums;
using CookBook.Shared.Interfaces;

namespace CookBook.BL.Facades.DTOs
{
    public class RecipeDetailDTO : IId
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public TimeSpan Duration { get; set; }
        public FoodType FoodType { get; set; }
        public string Description { get; set; }
        public ICollection<IngredientAmountDTO> Ingredients { get; set; } = new List<IngredientAmountDTO>();
    }
}