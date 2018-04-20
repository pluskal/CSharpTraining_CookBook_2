﻿using CookBook.BL.Repository.Base;
using CookBook.DAL.Entities;

namespace CookBook.BL.Repository
{
    public class RecipeRepository : RepositoryBase<RecipeEntity>
    {
        public RecipeRepository(UnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}