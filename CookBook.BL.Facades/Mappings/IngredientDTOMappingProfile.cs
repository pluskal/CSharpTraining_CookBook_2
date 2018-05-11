using AutoMapper;
using CookBook.BL.Facades.DTOs;
using CookBook.DAL.Entities;

namespace CookBook.BL.Facades.Mappings
{
    public class IngredientDTOMappingProfile : Profile
    {
        public IngredientDTOMappingProfile()
        {
            this.CreateMap<IngredientEntity, IngredientDTO>();
            this.CreateMap<IngredientDTO, IngredientEntity>();

            this.CreateMap<IngredientAmountEntity, IngredientAmountDTO>();
            this.CreateMap<IngredientAmountDTO, IngredientAmountEntity>();
        }
    }
}