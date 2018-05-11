using AutoMapper;
using CookBook.BL.Facades.DTOs;
using CookBook.DAL.Entities;
using CookBook.UI.WPFApp.Models;

namespace CookBook.UI.WPFApp.Adapters.Mappings
{
    public class RecipeMappingProfile : Profile
    {
        public RecipeMappingProfile()
        {
            this.CreateMap<RecipeListDTO, RecipeList>();
            this.CreateMap<RecipeDetailDTO, RecipeDetail>();
            this.CreateMap<RecipeDetail, RecipeDetailDTO>();
        }
    }
}