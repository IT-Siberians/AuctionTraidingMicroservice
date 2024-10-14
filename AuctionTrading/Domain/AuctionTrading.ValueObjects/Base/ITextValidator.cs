namespace AuctionTrading.Domain.ValueObjects.Base
{
    /// <summary>
    /// Defines a method that implements the validation of the object.
    /// </summary>
    public interface ITextValidator : IValidator<string>
    {
        /// <summary>
        /// The Text's max length
        /// </summary>
        public int MAX_LENGTH { get; }

        /// <summary>
        /// The Text's min length
        /// </summary>
        public int MIN_LENGTH { get; }
    }
}
