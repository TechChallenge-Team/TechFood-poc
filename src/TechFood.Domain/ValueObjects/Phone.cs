using TechFood.Domain.Shared.ValueObjects;

namespace TechFood.Domain.ValueObjects;

public class Phone : ValueObject
{
    public Phone(
        string countryCode,
        string dDD,
        string? number)
    {
        CountryCode = countryCode;
        DDD = dDD;
        Number = number;
    }

    public string CountryCode { get; init; }
    public string DDD { get; init; }
    public string? Number { get; init; }

    public void IsValidPhoneNumber()
    {

    }
}
