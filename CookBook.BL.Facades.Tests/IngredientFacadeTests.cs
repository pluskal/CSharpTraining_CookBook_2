using System;
using AutoMapper;
using CookBook.BL.Facades.DTOs;
using CookBook.BL.Facades.Mappings;
using CookBook.BL.Repository;
using CookBook.DAL;
using Xunit;

namespace CookBook.BL.Facades.Tests
{
    /// <summary>
    ///     Integration tests
    /// </summary>
    public class IngredientFacadeTests : IDisposable
    {
        private readonly IngredientFacade _facadeSUT;
        private readonly UnitOfWork _unitOfWork;

        public IngredientFacadeTests()
        {
            var cookBookDbContext = new CookBookDbContext();
            _unitOfWork = new UnitOfWork(cookBookDbContext);
            var ingredientRepository = new IngredientRepository(_unitOfWork);
            var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<IngredientMappingProfile>()));
            _facadeSUT = new IngredientFacade(ingredientRepository, mapper);
        }

        public void Dispose()
        {
            _unitOfWork?.Dispose();
        }

        [Fact]
        public void _GetList_AnyIngredient()
        {
            //Arrange
            //Act
            var allIngredients = _facadeSUT.GetList();

            //Assert
            Assert.NotEmpty(allIngredients);
        }

        [Fact]
        public void _InitializeNew_IdIsEmpty()
        {
            //Act
            var ingredient = _facadeSUT.InitializeNew();

            //Assert
            Assert.Equal(Guid.Empty, ingredient.Id);
        }

        [Fact]
        public void Id_GetDetail_IngredientReturned()
        {
            //Arrange
            //Act
            var ingredient = _facadeSUT.GetDetail(Guid.Empty);

            //Assert
            Assert.NotNull(ingredient);
        }

        [Fact]
        public void NewIngredientDTO_Save_IngredientSaved()
        {
            //Arrange
            var ingredient = new IngredientDTO {Name = "Milk", Description = "3.5%"};

            //Act
            ingredient = _facadeSUT.Save(ingredient);

            //Assert
            Assert.NotEqual(Guid.Empty, ingredient.Id);
        }

        [Fact]
        public void ExistingIngredient_Save_IngredientUpdated()
        {
            //Arrange
            var random = new Random(DateTime.Now.Millisecond).Next();
            var ingredient = new IngredientDTO
            {
                Id = Guid.Parse("60923986-c08b-4798-9658-fd2c98ca8da4"),
                Name = "Milk",
                Description = $"{random}"
            };

            //Act
            ingredient = _facadeSUT.Save(ingredient);

            //Assert
            Assert.Equal(random.ToString(), ingredient.Description);
        }

        [Fact]
        public void _Delete_IngredientDeleted()
        {
            ////Arrange
            

            ////Act
            //ingredient = _facadeSUT.Delete();

            ////Assert
            //Assert.Equal(random.ToString(), ingredient.Description);
        }
    }
}