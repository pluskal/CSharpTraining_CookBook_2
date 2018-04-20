using CookBook.DAL.Entities.Base;
using CookBook.Shared.Enums;

namespace CookBook.DAL.Entities
{
    public class IngredientAmountEntity : EntityBase
    {
        public Unit Unit { get; set; }
        public int Amount { get; set; }
        public RecipeEntity Recipe { get; set; }
        public IngredientEntity Ingredient { get; set; }
    }
}