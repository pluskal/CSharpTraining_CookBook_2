using System;
using CookBook.DAL.Entities.Base;
using CookBook.Shared.Enums;
using CookBook.Shared.Interfaces;

namespace CookBook.BL.Facades.DTOs
{
    public class IngredientDetailDTO : IId
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Unit Unit { get; set; }
        public int Amount { get; set; }
    }
}