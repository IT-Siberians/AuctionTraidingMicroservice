using AuctionTrading.Domain.ValueObjects.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public int? MAX_LENGTH { get; }

        /// <summary>
        /// The Text's min length
        /// </summary>
        public int? MIN_LENGTH { get; }
    }
}
