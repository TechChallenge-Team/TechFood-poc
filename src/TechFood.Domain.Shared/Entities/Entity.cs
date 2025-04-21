using System;

namespace TechFood.Domain.Shared.Entities
{
    public class Entity
    {
        public Guid Id { get; private set; } = Guid.NewGuid();

        public void SetId(Guid id)
       => Id = id;
    }
}
