﻿using AuctionTrading.Domain.ValueObject.Base;
using AuctionTrading.Domain.ValueObject.Validation;
using System.Runtime.CompilerServices;

namespace AuctionTrading.Domain.ValueObject
{
    /// <summary>
    /// Represents type of the entity's user name.
    /// </summary>
    /// <param name="name">The user name of the entity.</param>
    public class Username(string name) : ValueObject<string>(new StringValidator(), name)
    {

    }
}
