using System;
using System.Linq;
using System.Linq.Expressions;
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

        protected override Expression<Func<RecipeEntity, object>>[] EntityIncludes { get; }
        =new Expression<Func<RecipeEntity, object>>[]
            {
                recipe => recipe.Ingredients,
                recipe => recipe.Ingredients.Select(ingredientAmount=>ingredientAmount.Ingredient)
            };
    }
}