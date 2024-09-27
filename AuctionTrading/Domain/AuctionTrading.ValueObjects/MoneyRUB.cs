using AuctionTrading.Domain.ValueObjects.Base;
using AuctionTrading.Domain.ValueObjects.Validators;

namespace AuctionTrading.Domain.ValueObjects
{
    /// <summary>
    /// Represents type of the money.
    /// </summary>
    /// <param name="amount">The amount of the money.</param>
    public class MoneyRub(decimal amountInRub) : ValueObject<decimal>(
        new MoneyAmountValidator(),
        Math.Round(amountInRub, 2, MidpointRounding.AwayFromZero))
    {
        public static MoneyRub operator +(MoneyRub m1, MoneyRub m2)
            => new MoneyRub(m1.Value + m1.Value);

        public static MoneyRub operator -(MoneyRub m1, MoneyRub m2)
            => new MoneyRub(m1.Value - m2.Value);

        public static bool operator >(MoneyRub m1, MoneyRub m2)
            => m1.Value > m2.Value;

        public static bool operator <(MoneyRub m1, MoneyRub m2)
            => m1.Value < m2.Value;

        public static bool operator >=(MoneyRub m1, MoneyRub m2)
            => m1.Value >= m2.Value;

        public static bool operator <=(MoneyRub m1, MoneyRub m2)
            => m1.Value <= m2.Value;

    }
}

