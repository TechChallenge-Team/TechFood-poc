using TechFood.Domain.Shared.Entities;

namespace TechFood.Domain.Entities
{
    public class CategoryChanged : IDomainEvent
    {
        public string Name { get; set; }
    }
}