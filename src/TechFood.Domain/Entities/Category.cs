using TechFood.Domain.Shared.Entities;

namespace TechFood.Domain.Entities;

public class Category : Entity
{
    public Category(string name)
        => Name = name;

    public string Name { get; private set; }
}
