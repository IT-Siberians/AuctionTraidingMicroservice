using AuctionTrading.Domain.Entities;
using AuctionTrading.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionTrading.Domain.Exceptions
{
    public class NotForeseenSituationForThisLotStatusException(AuctionLot lot, LotStatus status)
        : InvalidOperationException($"Not foreseen situation of adding a bid for this lot status {status} (id = {lot.Id})")
    {
        public AuctionLot AuctionLot => lot;
        public LotStatus Status => status;
    }
}
