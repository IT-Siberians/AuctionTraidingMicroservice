using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionTrading.Domain.ValueObjects.Exceptions
{
    internal class TitleShortValueException(string title, int minLength)
    : FormatException($"Title length {title} shorter than minimum allowed length {minLength}")
    {
        public string Title => title;
        public int MinLength => minLength;
    }
}
