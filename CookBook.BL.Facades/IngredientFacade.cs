using System;
using System.Collections.Generic;
using CookBook.BL.Repository;

namespace CookBook.BL.Facades
{
    public class IngredientFacade
    {
        private readonly IngredientRepository _ingredientRepository;

        public IngredientFacade(IngredientRepository ingredientRepository)
        {
            this._ingredientRepository = ingredientRepository;
        }
        public IEnumerable<IngredientListDTO> GetList()
        {
            return null;
            //TODO this._ingredientRepository.GetAll();
        }

        public IngredientDetailDTO GetDetail(Guid id)
        {
            return null;
            //TODO this._ingredientRepository.GetById(id);
        }
    }
}
