using TechFoodClean.Common.Exceptions;
using TechFoodClean.Domain.Enums;
using TechFoodClean.Domain.Validations;

namespace TechFoodClean.Domain.ValueObjects;

public class Document : ValueObject
{
    public Document(
        DocumentType type,
        string value)
    {
        Type = type;
        Value = value;
        Validate();
    }
    public DocumentType Type { get; set; }

    public string Value { get; set; }

    private void Validate()
    {
        if (Type == DocumentType.CPF && !ValidaDocument.ValidarCPF(Value))
        {
            throw new DomainException(Common.Resources.Exceptions.Customer_ThrowDocumentCPFInvalid);
        }
    }
}
