using AutoMapper;
using CookBook.BL.Facades.DTOs;
using CookBook.DAL.Entities;
using CookBook.UI.WPFApp.Models;

namespace CookBook.UI.WPFApp.Adapters.Mappings
{
    public class IngredientMappingProfile : Profile
    {
        public IngredientMappingProfile()
        {
            this.CreateMap<Ingredient, IngredientDTO>();
            this.CreateMap<IngredientDTO, Ingredient>();

            this.CreateMap<IngredientAmount, IngredientAmountDTO>();
            this.CreateMap<IngredientAmountDTO, IngredientAmount>();
        }
    }
}