using System;
using CookBook.DAL.Entities.Base;
using CookBook.Shared.Interfaces;

namespace CookBook.BL.Facades.DTOs
{
    public class IngredientListDTO : IId
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}