using AuctionTrading.Domain.ValueObjects.Base;
using AuctionTrading.Domain.ValueObjects.Validators;

namespace AuctionTrading.Domain.ValueObjects
{
    /// <summary>
    /// Represents type of the money.
    /// </summary>
    /// <param name="amount">The amount of the money.</param>
    public class Money(decimal amountInRub) : ValueObject<decimal>(
        new MoneyAmountValidator(),
        Math.Round(amountInRub, 2, MidpointRounding.AwayFromZero))
    {
        public static Money operator +(Money m1, Money m2)
            => new(m1.Value + m2.Value);

        public static Money operator -(Money m1, Money m2)
            => new(m1.Value - m2.Value);

        public static bool operator >(Money m1, Money m2)
            => m1.Value > m2.Value;

        public static bool operator <(Money m1, Money m2)
            => m1.Value < m2.Value;

        public static bool operator >=(Money m1, Money m2)
            => m1.Value >= m2.Value;

        public static bool operator <=(Money m1, Money m2)
            => m1.Value <= m2.Value;

    }
}

