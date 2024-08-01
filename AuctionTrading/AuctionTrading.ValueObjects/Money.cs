using AuctionTrading.Domain.ValueObject.Base;
using AuctionTrading.Domain.ValueObject.Validation;
using System.Diagnostics.CodeAnalysis;

namespace AuctionTrading.Domain.ValueObject
{
    /// <summary>
    /// Represents type of the money.
    /// </summary>
    /// <param name="amount">The amount of the money.</param>
    public class Money : ValueObject<decimal>, IEqualityComparer<Money>, IComparable<Money>
    {
        private const int KopecFactor = 100;
        private readonly decimal amountInRubles;
        private Money(decimal amountInRub) : base (new DecimalValidator(), amountInRub)
        {
            amountInRubles = Decimal.Round(amountInRub, 2);
        }
        private Money(long amountInKopecs) : base(new DecimalValidator(), amountInKopecs)
        {
            amountInRubles = (decimal)amountInKopecs / KopecFactor;
        }
        public static Money FromKopecs(long amountInKopecs)
        {
            return new Money(amountInKopecs);
        }
        public static Money FromRubles(decimal amountInRubles)
        {
            return new Money(amountInRubles);
        }
        public decimal AmountInRubles
        {
            get { return amountInRubles; }
        }
        public long AmountInKopecs
        {
            get { return (int)(amountInRubles * KopecFactor); }
        }
        public int CompareTo(Money other)
        {
            if (amountInRubles < other.amountInRubles) return -1;
            if (amountInRubles == other.amountInRubles) return 0;
            else return 1;
        }
        public bool Equals(Money x, Money y)
        {
            return x.Equals(y);
        }
        public Money Add(Money other)
        {
            return new Money(amountInRubles + other.amountInRubles);
        }
        public Money Subtract(Money other)
        {
            return new Money(amountInRubles - other.amountInRubles);
        }
        public static Money operator +(Money m1, Money m2)
        {
            return m1.Add(m2);
        }
        public static Money operator -(Money m1, Money m2)
        {
            return m1.Subtract(m2);
        }
        public static bool operator >(Money m1, Money m2)
        {
            return m1.amountInRubles > m2.amountInRubles;
        }
        public static bool operator <(Money m1, Money m2)
        {
            return m1.amountInRubles < m2.amountInRubles;
        }
        public override bool Equals(object other)
        {
            return (other is Money) && Equals((Money)other);
        }
        public bool Equals(Money other)
        {
            return amountInRubles == other.amountInRubles;
        }

        public int GetHashCode([DisallowNull] Money obj)
        {
            return obj.GetHashCode();
        }
        public override int GetHashCode()
        {
            return (int)(AmountInKopecs ^ (AmountInKopecs >> 32));
        }
    }
}

