﻿namespace AuctionTrading.Domain.ValueObject.Exception
{
    /// <summary>
    /// Provides string constants for error messages.
    /// </summary>
    internal static class ExceptionMessage
    {
        public const string NOT_NULL_OR_WHITE_SPACE = "The data mustn't be null, empty or consists only of white-space characters";
        public const string VALIDATOR_MUST_BE_SPECIFIED = "Validator must be specified for type";
    }
}
