using AutoMapper;
using CookBook.BL.Facades.DTOs;
using CookBook.DAL.Entities;

namespace CookBook.BL.Facades.Mappings
{
    public class IngredientMappingProfile : Profile
    {
        public IngredientMappingProfile()
        {
            this.CreateMap<IngredientEntity, IngredientListDTO>();
            this.CreateMap<IngredientEntity, IngredientDetailDTO>();
            this.CreateMap<IngredientDetailDTO, IngredientEntity>();

            this.CreateMap<IngredientAmountEntity, IngredientDetailDTO>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Ingredient.Id))
                .ForMember(d => d.Name, opt => opt.MapFrom(src => src.Ingredient.Name))
                .ForMember(d => d.Description, opt => opt.MapFrom(src => src.Ingredient.Description));

            this.CreateMap<IngredientDetailDTO, IngredientAmountEntity>()
                .ForPath(d => d.Ingredient.Id, opt => opt.MapFrom(src => src.Id))
                .ForPath(d => d.Ingredient.Name, opt => opt.MapFrom(src => src.Name))
                .ForPath(d => d.Ingredient.Description, opt => opt.MapFrom(src => src.Description));
        }
    }
}