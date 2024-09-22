using AuctionTrading.Domain.ValueObjects.Base;
using AuctionTrading.Domain.ValueObjects.Exceptions;

namespace AuctionTrading.Domain.ValueObjects.Validators
{
    /// <summary>
    /// Defines a method that implements the validation of the decimal.
    /// </summary>
    public class MoneyAmountValidator : IValidator<decimal>
    {
        /// <summary>
        /// Verifies that the decimal is not negative and does not equal zero. 
        /// </summary>
        /// <param name="value">A decimal value.</param>
        /// <exception cref="ArgumentNullOrWhiteSpaceException"></exception>
        public void Validate(decimal value)
        {
            if (value <= 0)
                throw new MoneyAmountNonPositiveException(ExceptionMessage.MONEY_AMOUNT_NON_POSITIVE, nameof(value), value);
            if (!IsValidAmount(value))
                throw new MoneyAmountHasMoreThanTwoDecimalPlacesException(ExceptionMessage.MONEY_AMOUNT_HAS_NOT_MORE_THEN_TWO_DECIMAL_PLACES, nameof(value), value);
        }
        private bool IsValidAmount(decimal value)
        {
            value = value * 100;
            value -= (int)value;
            return value == 0m;
        }
    }
}
