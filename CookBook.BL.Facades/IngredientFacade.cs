using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Linq;
using CookBook.BL.Facades.DTOs;
using CookBook.BL.Repository;

namespace CookBook.BL.Facades
{
    public class IngredientFacade
    {
        private readonly IngredientRepository _ingredientRepository;
        private readonly Mapper _mapper;

        public IngredientFacade(IngredientRepository ingredientRepository, Mapper mapper)
        {
            this._ingredientRepository = ingredientRepository;
            _mapper = mapper;
        }
        public IEnumerable<IngredientListDTO> GetList()
        {
            return this._ingredientRepository.GetAll().Select(i=> _mapper.MapList(i));
        }
        
        public IngredientDetailDTO GetDetail(Guid id)
        {
            var ingredientEntity = this._ingredientRepository.GetById(id);
            if (ingredientEntity == null) return null;

            return _mapper.MapDetail(ingredientEntity);
        }

        public IngredientDetailDTO Save(IngredientDetailDTO ingredientDetailDTO)
        {
            var ingredientEntity = _mapper.Map(ingredientDetailDTO);

            if (ingredientDetailDTO.Id == Guid.Empty)
            {
                this._ingredientRepository.Insert(ingredientEntity);
            }
            else
            {
                this._ingredientRepository.Update(ingredientEntity);
            }

            return _mapper.MapDetail(ingredientEntity);
        }

        public IngredientDetailDTO InitializeNew()
        {
            return this._mapper.MapDetail(this._ingredientRepository.InitializeNew());
        }
    }
}
