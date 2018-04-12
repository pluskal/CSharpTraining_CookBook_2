namespace CookBook.DAL.Entities
{
    public class IngredientAmountEntity
    {
        public Unit Unit { get; set; }
        public int Amount { get; set; }
        public RecipeEntity Recipe { get; set; }
        public IngredientEntity Ingredient { get; set; }
    }
}