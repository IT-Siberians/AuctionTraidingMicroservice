using AuctionTrading.Domain.ValueObjects.Exceptions;

namespace AuctionTrading.Domain.ValueObjects.Base
{
    public abstract class ValueObject<T> : IEquatable<ValueObject<T>>
    {
        public T Value { get; }

        protected ValueObject(IValidator<T> validator, T value)
        {
            if (validator == null)
                throw new ValidatorNullException(GetType().FullName, ExceptionMessage.VALIDATOR_MUST_BE_SPECIFIED);
            validator.Validate(value);
            Value = value;
        }

        public override string ToString()
        {
            return Value?.ToString() ?? GetType().ToString();
        }

        public override int GetHashCode()
        {
            return Value!.GetHashCode();
        }

        public override bool Equals(object? other)
            => other is not null && Equals(other as ValueObject<T>);

        public bool Equals(ValueObject<T>? other)
        {
            if (other == null)
                return false;
            if (GetType() != other.GetType())
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return other.Value!.Equals(Value);
        }

        public static bool operator ==(ValueObject<T>? left, ValueObject<T>? right)
        {
            if ((object?)left == null || (object?)right == null)
                return Equals(left, right);
            return left.Equals(right);
        }

        public static bool operator !=(ValueObject<T>? left, ValueObject<T>? right)
        {
            return !(left == right);
        }
    }
}
