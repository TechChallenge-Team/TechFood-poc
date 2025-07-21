using System.Text.Json;

namespace TechFood.Infrastructure.Data.NamingPolicy;

public class UpperCaseNamingPolicy : JsonNamingPolicy
{
    public override string ConvertName(string name) => name.ToUpper();
}
