using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CookBook.DAL.Entities.Base;
using CookBook.Shared.Enums;

namespace CookBook.DAL.Entities
{
    public class IngredientAmountEntity
    {
        [Key, Column(Order = 1)]
        public Guid RecipeId { get; set; }
        [Key, Column(Order = 2)]
        public Guid IngredientId { get; set; }
        public Unit Unit { get; set; }
        public int Amount { get; set; }
        public RecipeEntity Recipe { get; set; }
        public IngredientEntity Ingredient { get; set; }
    }
}