using AuctionTrading.Domain.ValueObject.Exception;

namespace AuctionTrading.Domain.ValueObject.Base
{
    public abstract class ValueObject<T> : IEquatable<ValueObject<T>>
    {
        private readonly IValidator<T> _validator;
        public readonly T Value;
        protected ValueObject(IValidator<T> validator, T value)
        {
            if (validator == null)
                throw new ValidatorNotSpecifiedException(ExceptionMessage.VALIDATOR_MUST_BE_SPECIFIED, this.GetType().FullName);
            _validator = validator;
            _validator.Validate(value);
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
            => Equals(other as ValueObject<T>);

        public bool Equals(ValueObject<T>? other)
        {
            if (ReferenceEquals(this, other))
                return true;
            if (other == null)
                return false;
            if (GetType() != other.GetType())
                return false;
            return other.Value!.Equals(Value);
        }

        public static bool operator ==(ValueObject<T>? left, ValueObject<T>? right)
        {
            if (((object?)left) == null || ((object?)right) == null)
                return Object.Equals(left, right);
            return left.Equals(right);
        }

        public static bool operator !=(ValueObject<T>? left, ValueObject<T>? right)
        {
            if (((object?)left) == null || ((object?)right) == null)
                return !Object.Equals(left, right);
            return !left.Equals(right);
        }
    }
}
