﻿using AutoMapper;
using CookBook.BL.Facades.DTOs;
using CookBook.DAL.Entities;

namespace CookBook.BL.Facades.Mappings
{
    public class RecipeMappingProfile : Profile
    {
        public RecipeMappingProfile()
        {
            this.CreateMap<RecipeEntity, RecipeListDTO>();
            this.CreateMap<RecipeEntity, RecipeDetailDTO>();
            this.CreateMap<RecipeDetailDTO, RecipeEntity>();
        }
    }
}