using System;
using CookBook.Shared.Interfaces;

namespace CookBook.DAL.Entities.Base
{
    public abstract class EntityBase : IId
    {
        public Guid Id { get; set; }
    }
}