using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionTrading.Domain.Interfaces;

namespace AuctionTrading.Domain.Entities
{
    public class Seller : ISeller
    {
        public Guid Id { get; }
        public string Name { get; private set; }
        public Seller(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
        public bool ChangeName(string name)
        {
            throw new NotImplementedException();
        }

        public Task CancelLotAsync(AuctionLot lot)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AuctionLot>> GetAllLotsAsync(IEnumerable<AuctionLot> lots)
        {
            throw new NotImplementedException();
        }

        public Task<Bid> GetLastBidAsync(Guid lotId, IEnumerable<Bid> bids)
        {
            throw new NotImplementedException();
        }

        public Task<AuctionLot> GetLotDetailsAsync(Guid lotId, IEnumerable<AuctionLot> lots)
        {
            throw new NotImplementedException();
        }
    }
}
