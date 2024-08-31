using AuctionTrading.Domain.ValueObjects.Base;
using AuctionTrading.Domain.ValueObjects.Validators;

namespace AuctionTrading.Domain.ValueObjects
{
    /// <summary>
    /// Represents type of the money.
    /// </summary>
    /// <param name="amount">The amount of the money.</param>
    public class MoneyRUB(decimal amountInRub) : ValueObject<decimal>(new MoneyAmountValidator(), amountInRub)
    {
        public static MoneyRUB operator +(MoneyRUB m1, MoneyRUB m2)
        {
            return new MoneyRUB(m1.Value + m1.Value);
        }
        public static MoneyRUB operator -(MoneyRUB m1, MoneyRUB m2)
        {
            return new MoneyRUB(m1.Value - m2.Value);
        }
        public static bool operator >(MoneyRUB m1, MoneyRUB m2)
        {
            return m1.Value > m2.Value;
        }
        public static bool operator <(MoneyRUB m1, MoneyRUB m2)
        {
            return m1.Value < m2.Value;
        }
    }
}

