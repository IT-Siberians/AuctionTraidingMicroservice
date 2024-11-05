using AuctionTrading.Application.Models.Bid;
using AuctionTrading.Application.Models.Seller;
using AuctionTrading.Application.Services.Abstractions;
using AuctionTrading.Common.Enums;
using AuctionTrading.Domain.Entities;
using AuctionTrading.Domain.Repositories.Abstractions;
using AuctionTrading.Domain.ValueObjects;
using AutoMapper;
using Microsoft.VisualBasic;
using System.Net.Http.Headers;

namespace AuctionTrading.Application.Services
{
    public class BidderApplicationService(
        ICustomersRepository customersRepository,
        IAuctionLotRepository lotsRepository,
        IRepository<Bid, Guid> bidsRepository)
        : IBidderApplicationService
    {
        public async Task<BidStatus> MakeBidAsync(CreateBidModel bidInformation, CancellationToken cancellationToken = default)
        {
            var customer = await customersRepository.GetByIdAsync(bidInformation.CustomerId, cancellationToken);
            if (customer is null)
                return BidStatus.FaultedCustomerNotFound;

            var lot = await lotsRepository.GetByIdAsync(bidInformation.AuctionLotId, cancellationToken);
            if (lot is null)
                return BidStatus.FaultedLotNotFound;

            if (!lot.IsActive)
                return BidStatus.FaultedLotNotActive;

            if (customer.Id == lot.Seller.Id)
                return BidStatus.FaultedCreateBidOnYourLot;

            var bidStatus = customer.TryMakeBid(lot, new(bidInformation.Amount));

            if (bidStatus == BidStatus.Success)
            {
                var newBid = lot.LastBid;
                var result = await bidsRepository.AddAsync(newBid, cancellationToken);
                await lotsRepository.UpdateAsync(lot, cancellationToken);
                return result is not null?bidStatus:BidStatus.FaultedIncorrectBid;
            }

            return bidStatus;
        }
    }
}
