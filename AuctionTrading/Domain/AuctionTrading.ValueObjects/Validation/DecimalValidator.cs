using AuctionTrading.Domain.ValueObject.Base;
using AuctionTrading.Domain.ValueObject.Exception;
using System.Runtime.CompilerServices;

namespace AuctionTrading.Domain.ValueObject.Validation
{
    /// <summary>
    /// Defines a method that implements the validation of the decimal.
    /// </summary>
    public class DecimalValidator : IValidator<decimal>
    {
        /// <summary>
        /// Verifies that the decimal is not negative and does not equal zero. 
        /// </summary>
        /// <param name="value">A decimal value.</param>
        /// <exception cref="ArgumentNullOrWhiteSpaceException"></exception>
        public void Validate(decimal value)
        {
            if (value<=0)
                throw new ArgumentNonPositiveException(ExceptionMessage.NON_POSITIVE, nameof(value));
        }
    }
}
