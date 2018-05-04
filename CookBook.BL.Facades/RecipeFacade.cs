using System;
using AutoMapper;
using CookBook.BL.Facades.DTOs;
using CookBook.BL.Repository;
using CookBook.DAL.Entities;

namespace CookBook.BL.Facades
{
    public class RecipeFacade : CrudFacade<RecipeEntity, RecipeListDTO, RecipeDetailDTO>
    {
        public RecipeFacade(RecipeRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}