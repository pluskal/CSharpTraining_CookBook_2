using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CookBook.BL.Facades.DTOs;
using CookBook.BL.Facades.Mappings;
using CookBook.DAL.Entities;
using CookBook.Shared.Enums;
using Xunit;

namespace CookBook.BL.Facades.Tests
{
    public class RecipeMappingTests
    {
        private readonly IMapper _mapper;

        public RecipeMappingTests()
        {
            _mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<RecipeDTOMappingProfile>();
                cfg.AddProfile<IngredientDTOMappingProfile>();
            }));
            

            RecipeEntity = new RecipeEntity
            {
                Name = nameof(RecipeEntity.Name),
                Description = nameof(RecipeEntity.Description),
                Duration = TimeSpan.FromSeconds(323),
                FoodType = FoodType.MainCourse
            };
            IngredientEntity = new IngredientEntity
            {
                Name = nameof(IngredientEntity.Name),
                Description = nameof(IngredientEntity.Description)
            };
            IngredientAmountEntity = new IngredientAmountEntity
            {
                Recipe = RecipeEntity,
                RecipeId = RecipeEntity.Id,
                Ingredient = IngredientEntity,
                IngredientId = IngredientEntity.Id,
                Amount = 2,
                Unit = Unit.LittleSpoon
            };
            RecipeEntity.Ingredients.Add(IngredientAmountEntity);

            RecipeDetailDTO = new RecipeDetailDTO()
            {
                Id = RecipeEntity.Id,
                Name = RecipeEntity.Name,
                Description = RecipeEntity.Description,
                Duration = RecipeEntity.Duration,
                FoodType = RecipeEntity.FoodType,
                Ingredients = new List<IngredientAmountDTO>()
                {
                    new IngredientAmountDTO()
                    {
                        RecipeId = RecipeEntity.Id,
                        IngredientId = IngredientEntity.Id,
                        IngredientName = IngredientEntity.Name,
                        IngredientDescription = IngredientEntity.Description,
                        Amount = IngredientAmountEntity.Amount,
                        Unit = IngredientAmountEntity.Unit
                    }
                }
            };
        }

        public IngredientAmountEntity IngredientAmountEntity { get; }
        public IngredientEntity IngredientEntity { get; }

        public RecipeEntity RecipeEntity { get; }
        public RecipeDetailDTO RecipeDetailDTO { get; }

        [Fact]
        public void RecipeEntity_RecipeDetailDTO()
        {
            //Arrange
            //Act
            var detailDTO = _mapper.Map<RecipeDetailDTO>(RecipeEntity);

            //Assert
            Assert.Equal(RecipeEntity.Id, detailDTO.Id);
            Assert.Equal(RecipeEntity.Name, detailDTO.Name);
            Assert.Equal(RecipeEntity.Description, detailDTO.Description);
            Assert.Equal(RecipeEntity.Duration, detailDTO.Duration);
            Assert.Equal(RecipeEntity.FoodType, detailDTO.FoodType);

            var ingredientAmountDTO = detailDTO.Ingredients.First();
            Assert.Equal(RecipeEntity.Id, ingredientAmountDTO.RecipeId);
            Assert.Equal(IngredientEntity.Id, ingredientAmountDTO.IngredientId);
            Assert.Equal(IngredientEntity.Name, ingredientAmountDTO.IngredientName);
            Assert.Equal(IngredientEntity.Description, ingredientAmountDTO.IngredientDescription);
            Assert.Equal(IngredientAmountEntity.Amount, ingredientAmountDTO.Amount);
            Assert.Equal(IngredientAmountEntity.Unit, ingredientAmountDTO.Unit);
        }

        [Fact]
        public void RecipeEntity_RecipeListDTO()
        {
            //Arrange
            //Act
            var detailDTO = _mapper.Map<RecipeListDTO>(RecipeEntity);

            //Assert
            Assert.Equal(RecipeEntity.Id, detailDTO.Id);
            Assert.Equal(RecipeEntity.Name, detailDTO.Name);
            Assert.Equal(RecipeEntity.Duration, detailDTO.Duration);
            Assert.Equal(RecipeEntity.FoodType, detailDTO.FoodType);
        }

        [Fact]
        public void RecipeDetailDTO_RecipeEntity()
        {
            //Arrange
            //Act
            var entity = _mapper.Map<RecipeEntity>(RecipeDetailDTO);

            //Assert
            Assert.Equal(RecipeDetailDTO.Id, entity.Id);
            Assert.Equal(RecipeDetailDTO.Name, entity.Name);
            Assert.Equal(RecipeDetailDTO.Description, entity.Description);
            Assert.Equal(RecipeDetailDTO.Duration, entity.Duration);
            Assert.Equal(RecipeEntity.FoodType, entity.FoodType);

            var ingredientAmountEntity = entity.Ingredients.First();

            Assert.Equal(RecipeDetailDTO.Id, ingredientAmountEntity.RecipeId);
            Assert.Equal(IngredientEntity.Id, ingredientAmountEntity.IngredientId);
            Assert.Equal(IngredientAmountEntity.Amount, ingredientAmountEntity.Amount);
            Assert.Equal(IngredientAmountEntity.Unit, ingredientAmountEntity.Unit);

            Assert.Equal(IngredientEntity.Id, ingredientAmountEntity.Ingredient.Id);
            Assert.Equal(IngredientEntity.Name, ingredientAmountEntity.Ingredient.Name);
            Assert.Equal(IngredientEntity.Description, ingredientAmountEntity.Ingredient.Description);
        }
    }
}