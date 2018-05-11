using System;
using AutoMapper;
using CookBook.BL.Facades.DTOs;
using CookBook.BL.Facades.Mappings;
using CookBook.DAL.Entities;
using Xunit;

namespace CookBook.BL.Facades.Tests
{
    public class IngredientMappingTests
    {
        public IngredientMappingTests()
        {
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<IngredientDTOMappingProfile>()));

            IngredientEntity = new IngredientEntity
            {
                Id = Guid.Parse("60923986-c08b-4798-9658-fd2c98ca8da4"),
                Name = nameof(IngredientEntity.Name),
                Description = nameof(IngredientEntity.Description)
            };
            IngredientDTO = new IngredientDTO
            {
                Id = Guid.Parse("60923986-c08b-4798-9658-fd2c98ca8da4"),
                Name = nameof(IngredientEntity.Name),
                Description = nameof(IngredientEntity.Description)
            };
        }

        private readonly IMapper _mapper;
        public IngredientEntity IngredientEntity { get; }
        public IngredientDTO IngredientDTO { get; }

        [Fact]
        public void IngredientDTO_IngredientEntity()
        {
            //Arrange
            //Act
            var entity = _mapper.Map<IngredientEntity>(IngredientDTO);

            //Assert
            Assert.Equal(IngredientEntity.Id, entity.Id);
            Assert.Equal(IngredientEntity.Name, entity.Name);
            Assert.Equal(IngredientEntity.Description, entity.Description);
        }

        [Fact]
        public void IngredientEntity_IngredientLiDTO()
        {
            //Arrange
            //Act
            var listDto = _mapper.Map<IngredientDTO>(IngredientEntity);

            //Assert
            Assert.Equal(IngredientEntity.Id, listDto.Id);
            Assert.Equal(IngredientEntity.Name, listDto.Name);
            Assert.Equal(IngredientEntity.Description, listDto.Description);
        }
    }
}