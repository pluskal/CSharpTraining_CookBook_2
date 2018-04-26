using System;
using AutoMapper;
using CookBook.BL.Facades.DTOs;
using CookBook.DAL.Entities;
using Xunit;

namespace CookBook.BL.Facades.Tests
{
    public class MappingTests
    {
        public MappingTests()
        {
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<CookBookMappingProfile>()));

            IngredientEntity = new IngredientEntity
            {
                Id = Guid.Parse("60923986-c08b-4798-9658-fd2c98ca8da4"),
                Name = nameof(IngredientEntity.Name),
                Description = nameof(IngredientEntity.Description)
            };
            IngredientDetailDTO = new IngredientDetailDTO
            {
                Id = Guid.Parse("60923986-c08b-4798-9658-fd2c98ca8da4"),
                Name = nameof(IngredientEntity.Name),
                Description = nameof(IngredientEntity.Description)
            };
        }

        private readonly IMapper _mapper;
        public IngredientEntity IngredientEntity { get; }
        public IngredientDetailDTO IngredientDetailDTO { get; }

        [Fact]
        public void IngredientEntity_IngredientDetailDTO()
        {
            //Arrange
            //Act
            var detailDTO = _mapper.Map<IngredientDetailDTO>(IngredientEntity);

            //Assert
            Assert.Equal(IngredientEntity.Id, detailDTO.Id);
            Assert.Equal(IngredientEntity.Name, detailDTO.Name);
            Assert.Equal(IngredientEntity.Description, detailDTO.Description);
        }

        [Fact]
        public void IngredientDetailDTO_IngredientEntity()
        {
            //Arrange
            //Act
            var entity = _mapper.Map<IngredientEntity>(IngredientDetailDTO);

            //Assert
            Assert.Equal(IngredientEntity.Id, entity.Id);
            Assert.Equal(IngredientEntity.Name, entity.Name);
            Assert.Equal(IngredientEntity.Description, entity.Description);
        }

        [Fact]
        public void IngredientEntity_IngredientListDTO()
        {
            //Arrange
            //Act
            var listDto = _mapper.Map<IngredientListDTO>(IngredientEntity);

            //Assert
            Assert.Equal(IngredientEntity.Id, listDto.Id);
            Assert.Equal(IngredientEntity.Name, listDto.Name);
            Assert.Equal(IngredientEntity.Description, listDto.Description);
        }
    }
}