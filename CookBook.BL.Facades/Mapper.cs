using CookBook.BL.Facades.DTOs;
using CookBook.DAL.Entities;

namespace CookBook.BL.Facades
{
    public class Mapper
    {
        public IngredientListDTO MapList(IngredientEntity ingredientEntity)
        {
            return new IngredientListDTO()
            {
                Id = ingredientEntity.Id,
                Name = ingredientEntity.Name,
                Description = ingredientEntity.Description
            };
        }

        public IngredientDetailDTO MapDetail(IngredientEntity ingredientEntity)
        {
            return new IngredientDetailDTO()
            {
                Id = ingredientEntity.Id,
                Name = ingredientEntity.Name,
                Description = ingredientEntity.Description
            };
        }

        public IngredientEntity Map(IngredientDetailDTO ingredientDetailDTO)
        {
            return new IngredientEntity()
            {
                Id = ingredientDetailDTO.Id,
                Name = ingredientDetailDTO.Name,
                Description = ingredientDetailDTO.Description
            };
        }
    }
}