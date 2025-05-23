using TechFood.Domain.Shared.ValueObjects;

namespace TechFood.Domain.ValueObjects;

public class Name : ValueObject
{
    public Name(string fullName)
    {
        FullName = fullName;
    }

    public string FullName { get; private set; } = null!;

    //public string First { get; private set; }

    //public string? Middle { get; private set; }

    //public string Last { get; private set; }

    //public static implicit operator Name(string note) => new(note);

    //public static implicit operator string(Name name)
    //{
    //    return string.Join(" ",[name.First, name.Middle, name.Last]);
    //}
}
