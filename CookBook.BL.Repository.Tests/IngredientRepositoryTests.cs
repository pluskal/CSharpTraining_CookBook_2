using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using CookBook.DAL;
using CookBook.DAL.Entities;
using Moq;
using Xunit;

namespace CookBook.BL.Repository.Tests
{
    public class IngredientRepositoryTests
    {
        private readonly IngredientRepository _repository;
        private readonly Mock<CookBookDbContext> _dbContextMock = new Mock<CookBookDbContext>();
        private readonly List<IngredientEntity> _ingredientsData = new List<IngredientEntity>();

        public IngredientRepositoryTests()
        {
            var data = this._ingredientsData.AsQueryable();

            var ingredientDbSetMock = new Mock<DbSet<IngredientEntity>>();
            ingredientDbSetMock.As<IQueryable<IngredientEntity>>().Setup(m => m.Provider).Returns(data.Provider);
            ingredientDbSetMock.As<IQueryable<IngredientEntity>>().Setup(m => m.Expression).Returns(data.Expression);
            ingredientDbSetMock.As<IQueryable<IngredientEntity>>().Setup(m => m.ElementType).Returns(data.ElementType);
            ingredientDbSetMock.As<IQueryable<IngredientEntity>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            this._dbContextMock.Setup(c => c.Ingredients).Returns(ingredientDbSetMock.Object);

            this._repository = new IngredientRepository(this._dbContextMock.Object);
        }

        [Fact]
        public void Empty_GetAll_IsEmpty()
        {
            //Arrange
            //Act
            var allIngredients = this._repository.GetAll();

            //Assert
            Assert.Empty(allIngredients);
        }

        [Fact]
        public void OneIngredient_GetAll_IsNotEmpty()
        {
            //Arrange
            this._ingredientsData.Add(new IngredientEntity());
            //Act
            var allIngredients = this._repository.GetAll();

            //Assert
            Assert.NotEmpty(allIngredients);
        }

        //private Mock<DbSet<TEntity>> CreateDbSet<TEntity>(IList<TEntity> entityList) where TEntity : class
        //{
        //    var data = entityList.AsQueryable();
        //    var ingredientDbSetMock = new Mock<DbSet<TEntity>>();
        //    ingredientDbSetMock.As<IQueryable<TEntity>>().Setup(m => m.Provider).Returns(data.Provider);
        //    ingredientDbSetMock.As<IQueryable<TEntity>>().Setup(m => m.Expression).Returns(data.Expression);
        //    ingredientDbSetMock.As<IQueryable<TEntity>>().Setup(m => m.ElementType).Returns(data.ElementType);
        //    ingredientDbSetMock.As<IQueryable<TEntity>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

        //    return ingredientDbSetMock;
        //}
    }
}
