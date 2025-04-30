using TechFood.Domain.Shared.Exceptions;

namespace TechFood.Domain.Shared.Validations
{
    public class Validations
    {
        public static void ThrowIfEmpty(string value, string message)
        {
            if (value == null || value.Trim().Length == 0)
            {
                throw new DomainException(message);
            }
        }
    }
}
