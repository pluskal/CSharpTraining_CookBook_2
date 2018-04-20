using CookBook.Shared.Enums;

namespace CookBook.BL.Facades
{
    public class IngredientDetailDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Unit Unit { get; set; }
        public int Amount { get; set; }
    }
}