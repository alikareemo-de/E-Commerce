using System.Text.RegularExpressions;

namespace ECommerce.Domain.ValueObjects
{
    public class Email
    {
        public string Value { get; }

        private static readonly Regex _emailRegex =
            new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);

        public Email(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || !_emailRegex.IsMatch(value))
            {
                throw new ArgumentException("Invalid email address", nameof(value));
            }

            Value = value;
        }

        // Implicit conversion to string
        public static implicit operator string(Email email) => email.Value;

        public override string ToString() => Value;

        public override bool Equals(object? obj)
        {
            return obj is Email other && Value.Equals(other.Value, StringComparison.OrdinalIgnoreCase);
        }

        public override int GetHashCode() => Value.ToLowerInvariant().GetHashCode();
    }
}
