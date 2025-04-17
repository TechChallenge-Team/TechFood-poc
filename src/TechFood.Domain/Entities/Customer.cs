using TechFood.Domain.Shared.Entities;
using TechFood.Domain.ValueObjects;

namespace TechFood.Domain.Entities;

public class Customer : Entity
{
    public Customer(Name name, Email email, Document document, Phone? phone)
    {
        Name = name;
        Email = email;
        Document = document;
        Phone = phone;
    }

    public Name Name { get; private set; }

    public Email Email { get; private set; }

    public Document Document { get; private set; }

    public Phone? Phone { get; private set; }
}
