using TechFood.Domain.Shared.Entities;
using TechFood.Domain.Shared.Validations;

namespace TechFood.Domain.Entities;

public class Category : Entity, IAggregateRoot
{
    public Category() { }

    public Category(string name, string imageFileName)
    {
        Name = name;
        ImageFileName = imageFileName;

        Validate();
    }

    public string Name { get; private set; } = null!;

    public string ImageFileName { get; private set; } = null!;

    public void UpdateAsync(string name, string imageFileName)
    {
        Name = name;
        ImageFileName = imageFileName;

        Validate();
    }

    private void Validate()
    {
        Validations.ThrowIfEmpty(Name, Resources.Exceptions.Category_ThrowNameIsEmpty);
        Validations.ThrowIfEmpty(ImageFileName, Resources.Exceptions.Category_ThrowFileImageIsEmpty);
    }
}
