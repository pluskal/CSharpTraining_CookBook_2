using System;

namespace CookBook.BL.Facades.DTOs
{
    public class IngredientListDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}