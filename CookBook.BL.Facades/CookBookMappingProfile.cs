using AutoMapper;
using AutoMapper.Mappers;
using CookBook.BL.Facades.DTOs;
using CookBook.DAL.Entities;

namespace CookBook.BL.Facades
{
    public class CookBookMappingProfile : Profile
    {
        public CookBookMappingProfile()
        {
            this.CreateMap<IngredientEntity, IngredientListDTO>();
            this.CreateMap<IngredientEntity, IngredientDetailDTO>();
            this.CreateMap<IngredientDetailDTO, IngredientEntity>();
        }
    }
}