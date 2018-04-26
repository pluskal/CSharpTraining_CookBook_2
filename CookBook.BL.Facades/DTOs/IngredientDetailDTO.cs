using System;
using CookBook.Shared.Enums;

namespace CookBook.BL.Facades.DTOs
{
    public class IngredientDetailDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Unit Unit { get; set; }
        public int Amount { get; set; }
    }
}