using AuctionTrading.Domain.ValueObjects.Base;
using AuctionTrading.Domain.ValueObjects.Exceptions;

namespace AuctionTrading.Domain.ValueObjects.Validators
{
    /// <summary>
    /// Defines a method that implements the validation of the string.
    /// </summary>
    public class DescriptionValidator : ITextValidator
    {
        /// <summary>
        /// The Description's min length
        /// </summary>
        public int MIN_LENGTH => 0;

        /// <summary>
        /// The Description's max length
        /// </summary>
        public int MAX_LENGTH => 0;

        /// <summary>
        /// Verifies the string to make sure it is not null, empty or doesn't consists only white-space characters. 
        /// </summary>
        /// <param name="value">A string containing data.</param>
        /// <exception cref="ArgumentNullOrWhiteSpaceException"></exception>
        public void Validate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullOrWhiteSpaceException(nameof(value), ExceptionMessages.TITLE_NOT_NULL_OR_WHITE_SPACE);
        }
    }
}
