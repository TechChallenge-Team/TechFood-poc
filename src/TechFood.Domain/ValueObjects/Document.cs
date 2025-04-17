using TechFood.Domain.Enums;
using TechFood.Domain.Shared.ValueObjects;

namespace TechFood.Domain.ValueObjects;

public class Document : ValueObject
{
    public Document(
        DocumentType type,
        string value)
    {
        Type = type;
        Value = value;
    }

    public DocumentType Type { get; set; }
    public string Value { get; set; }
}
