using TechFood.Domain.Shared.Exceptions;
using TechFood.Domain.Shared.Validations;
using TechFood.Domain.Shared.ValueObjects;

namespace TechFood.Domain.ValueObjects;

public class Email : ValueObject
{
    public Email(string address)
    {
        Address = address;
        Validate();
    }

    public string Address { get; set; }

    public static implicit operator Email(string address) => new(address);

    public static implicit operator string(Email email) => email.Address;
    private void Validate()
    {
        if (!ValidateEmail.IsValidEmail(Address))
        {
            throw new DomainException(Resources.Exceptions.Customer_ThrowEmailInvalid);
        }
    }
}
