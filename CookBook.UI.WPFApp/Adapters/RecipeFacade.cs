using System;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using CookBook.BL.Facades;
using CookBook.BL.Facades.DTOs;
using CookBook.BL.Repository;
using CookBook.DAL.Entities;
using CookBook.UI.WPFApp.Models;

namespace CookBook.UI.WPFApp.Adapters
{
    public class RecipeFacadeAdapter : CrudFacadeAdapter
        <RecipeEntity, RecipeListDTO, RecipeDetailDTO, RecipeList, RecipeDetail>
    {
        public RecipeFacadeAdapter(RecipeFacade facade, IMapper mapper) : base(facade, mapper)
        {
        }
    }
}