using System;

namespace CookBook.DAL.Entities.Base
{
    public abstract class EntityBase
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        protected bool Equals(EntityBase other)
        {
            return this.Id.Equals(other.Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((EntityBase) obj);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}