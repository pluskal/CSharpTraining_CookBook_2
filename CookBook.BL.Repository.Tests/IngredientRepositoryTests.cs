﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using CookBook.DAL;
using CookBook.DAL.Entities;
using CookBook.DAL.Entities.Base;
using Moq;
using Xunit;

namespace CookBook.BL.Repository.Tests
{
    public class IngredientRepositoryTests
    {
        private readonly IngredientRepository _repositorySUT;
        private readonly Mock<CookBookDbContext> _dbContextMock = new Mock<CookBookDbContext>();
        private readonly Mock<TestDbSet<IngredientEntity>> _ingredientDbSetMock;

        private static readonly IngredientEntity[] IngredientSeed =
        {
            new IngredientEntity {Name = "Salt", Description = $"Salty"},
            new IngredientEntity {Name = "Wheat", Description = $""},
            new IngredientEntity {Name = "Water", Description = $""},
            new IngredientEntity {Name = "Ice", Description = $""},
            new IngredientEntity {Name = "Milk", Description = $""},
        };

        public IngredientRepositoryTests()
        {
            this._ingredientDbSetMock = this.CreateDbSet(IngredientSeed);
            
            this._dbContextMock.Setup(c => c.Ingredients).Returns(this._ingredientDbSetMock.Object);

            this._repositorySUT = new IngredientRepository(this._dbContextMock.Object);
        }

        [Fact]
        public void Empty_GetAll_IsEmpty()
        {
            //Arrange
            this._ingredientDbSetMock.Object.Local.Clear();
            //Act
            var allIngredients = this._repositorySUT.GetAll();

            //Assert
            Assert.Empty(allIngredients);
        }

        [Fact]
        public void OneIngredient_GetAll_IsNotEmpty()
        {
            //Arrange
            //Act
            var allIngredients = this._repositorySUT.GetAll();

            //Assert
            Assert.NotEmpty(allIngredients);
        }

        [Fact]
        public void NewEntity_Insert_EntityInserted()
        {
            //Arrange
            var ingredientEntity = new IngredientEntity();
            //Act
            this._repositorySUT.Insert(ingredientEntity);

            //Assert
            this._ingredientDbSetMock.Verify(set => set.Add(ingredientEntity), Times.Once);
        }

        [Fact]
        public void NewEntities_Insert_EntitiesInserted()
        {
            //Arrange
            var ingredientEntity1 = new IngredientEntity(){Name = "Entity1Name",Description = "Entity1Description" };
            var ingredientEntity2 = new IngredientEntity(){Name = "Entity2Name",Description = "Entity2Description" };
            var ingredientEntity3 = new IngredientEntity(){Name = "Entity3Name",Description = "Entity3Description" };

            //Act
            this._repositorySUT.Insert(ingredientEntity1);
            this._repositorySUT.Insert(ingredientEntity2);
            this._repositorySUT.Insert(ingredientEntity3);

            //Assert
            this._ingredientDbSetMock.Verify(set => set.Add(ingredientEntity1), Times.Once);
            this._ingredientDbSetMock.Verify(set => set.Add(ingredientEntity2), Times.Once);
            this._ingredientDbSetMock.Verify(set => set.Add(ingredientEntity3), Times.Once);

            Assert.Contains(ingredientEntity1, this._ingredientDbSetMock.Object.Local);
            Assert.Contains(ingredientEntity2, this._ingredientDbSetMock.Object.Local);
            Assert.Contains(ingredientEntity3, this._ingredientDbSetMock.Object.Local);
        }

        [Fact]
        public void OneIngredient_Delete_SetEntityStateToModified()
        {
            //Arrange
            var deletedIngredient = IngredientSeed[1];

            //Act
            this._repositorySUT.Delete(deletedIngredient);

            //Assert
            Assert.DoesNotContain(deletedIngredient, this._ingredientDbSetMock.Object.Local);
            this._ingredientDbSetMock.Verify(set => set.Remove(deletedIngredient), Times.Once);

        }

        [Fact]
        public void MultipleIngredients_Delete_ShouldDeleteItems()
        {
            //Arrange
            var deletedIngredient1 = IngredientSeed[1];
            var deletedIngredient2 = IngredientSeed[2];
            var deletedIngredient3 = IngredientSeed[3];

            //Act
            this._repositorySUT.Delete(deletedIngredient1);
            this._repositorySUT.Delete(deletedIngredient2);
            this._repositorySUT.Delete(deletedIngredient3);

            //Assert
            Assert.DoesNotContain(deletedIngredient1, this._ingredientDbSetMock.Object.Local);
            this._ingredientDbSetMock.Verify(set=>set.Remove(deletedIngredient1),Times.Once);

            Assert.DoesNotContain(deletedIngredient2, this._ingredientDbSetMock.Object.Local);
            this._ingredientDbSetMock.Verify(set => set.Remove(deletedIngredient2), Times.Once);

            Assert.DoesNotContain(deletedIngredient3, this._ingredientDbSetMock.Object.Local);
            this._ingredientDbSetMock.Verify(set => set.Remove(deletedIngredient2), Times.Once);

        }

        [Fact]
        public void OneItem_DeleteById_ItemIsDeleted()
        {
            //Arrange
            var deletedIngredient = IngredientSeed[1];

            //Act
            this._repositorySUT.Delete(deletedIngredient.Id);

            //Assert
            this._ingredientDbSetMock.Verify(set => set.Remove(It.Is<IngredientEntity>(entity => entity.Id == deletedIngredient.Id)), Times.Once);
            Assert.DoesNotContain(deletedIngredient, this._ingredientDbSetMock.Object.Local);

        }

        [Fact]
        public void OneItemNonExisting_DeleteById_ShouldCallRemove()
        {
            //Arrange
            var deletedIngredient = new IngredientEntity();

            //Act
            this._repositorySUT.Delete(deletedIngredient.Id);

            //Assert
            this._ingredientDbSetMock.Verify(set => set.Remove(It.Is<IngredientEntity>(entity => entity.Id == deletedIngredient.Id)), Times.Once);
        }

        [Fact]
        public void OneItem_GetById_ReturnsCorrectItem()
        {
            //Arrange
            var id = IngredientSeed[0].Id;
            var expectedIngredient = IngredientSeed.Single(entity => entity.Id == id);

            //Act
            var ingredient = this._repositorySUT.GetById(id);

            Assert.Equal(expectedIngredient, ingredient);
        }

        [Fact(Skip = "Entry is not virtual")]
        public void OneItem_Update_ItemUpdated()
        {
            //Arrange
            var updatedIngredient = IngredientSeed[0];
            updatedIngredient.Name = "Updated name";
            updatedIngredient.Description = "Updated description";

            //Act
            this._repositorySUT.Update(updatedIngredient);

            //Assert
            this._dbContextMock.Verify(context => context.Entry(updatedIngredient), Times.Once);

        }

        private Mock<TestDbSet<TEntity>> CreateDbSet<TEntity>(IList<TEntity> entityList) where TEntity : EntityBase, new()
        {
            var dbSetMock = new Mock<TestDbSet<TEntity>>(entityList) { CallBase = true };
            var data = dbSetMock.Object.Local.AsQueryable();
            dbSetMock.As<IQueryable<TEntity>>().Setup(m => m.Provider).Returns(data.Provider);
            dbSetMock.As<IQueryable<TEntity>>().Setup(m => m.Expression).Returns(data.Expression);
            dbSetMock.As<IQueryable<TEntity>>().Setup(m => m.ElementType).Returns(data.ElementType);
            dbSetMock.As<IQueryable<TEntity>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator);

            return dbSetMock;
        }
    }
}
