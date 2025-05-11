using System.Linq;

namespace TechFood.Domain.Shared.Validations;

public class DocumentValidation
{
    public struct Cpf
    {
        private readonly string _value;

        public readonly bool IsValid;

        private Cpf(string value)
        {
            _value = value;
            IsValid = ValidateInternal(value);
        }

        private static bool ValidateInternal(string cpf)
        {
            if (cpf == null)
                return false;

            var digits = cpf
                .Where(char.IsDigit)
                .Select(c => c - '0')
                .ToArray();

            if (digits.Length != 11 || AllDigitsAreEqual(digits))
                return false;

            var firstCheckDigit = CalculateCheckDigit(digits, 10);
            if (digits[9] != firstCheckDigit)
                return false;

            var secondCheckDigit = CalculateCheckDigit(digits, 11, firstCheckDigit);
            return digits[10] == secondCheckDigit;
        }

        private static bool AllDigitsAreEqual(int[] digits)
            => digits.All(d => d == digits[0]);

        private static int CalculateCheckDigit(int[] digits, int weightStart, int? previousDigit = null)
        {
            var sum = 0;

            for (var i = 0; i < weightStart - 1; i++)
            {
                sum += digits[i] * (weightStart - i);
            }

            if (previousDigit.HasValue)
            {
                sum += previousDigit.Value * 2;
            }

            var remainder = sum % 11;
            return remainder < 2 ? 0 : 11 - remainder;
        }

        public static implicit operator Cpf(string value)
            => new Cpf(value);

        public override string ToString() => _value;
    }

    public static bool IsCpfValid(Cpf cpf)
        => cpf.IsValid;
}
