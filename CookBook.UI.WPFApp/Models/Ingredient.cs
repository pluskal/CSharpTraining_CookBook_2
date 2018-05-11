using System;
using CookBook.Shared.Interfaces;

namespace CookBook.UI.WPFApp.Models
{
    public class Ingredient : IId
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}