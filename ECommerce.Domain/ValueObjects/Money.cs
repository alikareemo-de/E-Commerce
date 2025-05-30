namespace ECommerce.Domain.ValueObjects
{
    public class Money
    {
        public decimal Amount { get; }
        public string Currency { get; }

        public Money(decimal amount, string currency = "USD")
        {
            if (amount < 0)
                throw new ArgumentException("Amount cannot be negative", nameof(amount));

            Amount = amount;
            Currency = currency;
        }

        private Money() { }
        public Money Add(Money other)
        {
            if (Currency != other.Currency)
                throw new InvalidOperationException("Cannot add money with different currencies.");

            return new Money(Amount + other.Amount, Currency);
        }

        public override string ToString() => $"{Amount} {Currency}";

        public override bool Equals(object? obj)
        {
            return obj is Money other &&
                   Amount == other.Amount &&
                   Currency == other.Currency;
        }

        public override int GetHashCode() => HashCode.Combine(Amount, Currency);
    }
}
