using AutoMapper;
using CookBook.BL.Facades.DTOs;
using CookBook.BL.Repository;
using CookBook.DAL.Entities;

namespace CookBook.BL.Facades
{
    public class IngredientFacade : CrudFacade<IngredientEntity, IngredientListDTO, IngredientDetailDTO>
    {
        public IngredientFacade(IngredientRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}