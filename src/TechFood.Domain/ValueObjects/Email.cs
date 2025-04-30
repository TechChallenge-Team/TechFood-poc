using TechFood.Domain.Shared.ValueObjects;

namespace TechFood.Domain.ValueObjects;

public class Email : ValueObject
{
    public Email(string address)
        => Address = address;

    public string Address { get; set; }

    public static implicit operator Email(string address) => new(address);

    public static implicit operator string(Email email) => email.Address;
}
