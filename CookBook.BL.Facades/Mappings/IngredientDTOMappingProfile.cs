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

            this.CreateMap<IngredientAmountEntity, IngredientAmountDTO>()
                .ForMember(d => d.IngredientName, opt => opt.MapFrom(src => src.Ingredient.Name))
                .ForMember(d => d.IngredientDescription, opt => opt.MapFrom(src => src.Ingredient.Description));

            this.CreateMap<IngredientAmountDTO, IngredientAmountEntity>()
                .ForPath(d => d.Ingredient.Id, opt => opt.MapFrom(src => src.IngredientId))
                .ForPath(d => d.Ingredient.Name, opt => opt.MapFrom(src => src.IngredientName))
                .ForPath(d => d.Ingredient.Description, opt => opt.MapFrom(src => src.IngredientDescription));
        }
    }
}