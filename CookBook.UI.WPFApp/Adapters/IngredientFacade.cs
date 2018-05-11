using AutoMapper;
using CookBook.BL.Facades;
using CookBook.BL.Facades.DTOs;
using CookBook.BL.Repository;
using CookBook.DAL.Entities;
using CookBook.UI.WPFApp.Models;

namespace CookBook.UI.WPFApp.Adapters
{
    public class IngredientFacadeAdapter : CrudFacadeAdapter
        <IngredientEntity, IngredientDTO, IngredientDTO, Ingredient, Ingredient>
    {
        public IngredientFacadeAdapter(IngredientFacade facade, IMapper mapper) : base(facade, mapper)
        {
        }
    }
}