using AuctionTrading.Application.Models.Bid;
using AuctionTrading.Application.Models.Seller;
using AuctionTrading.Application.Services.Abstractions;
using AuctionTrading.Common.Enums;
using AuctionTrading.Domain.Entities;
using AuctionTrading.Domain.Repositories.Abstractions;
using AuctionTrading.Domain.ValueObjects;
using AutoMapper;
using MassTransit;
using Microsoft.VisualBasic;
using Otus.QueueDto.Lot;
using System.Net.Http.Headers;

namespace AuctionTrading.Application.Services
{
    public class BidderApplicationService(
        ICustomersRepository customersRepository,
        IAuctionLotRepository lotsRepository,
        IRepository<Bid, Guid> bidsRepository,
        IBusControl busControl)
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

            bool isFirstBid = lot.LastBid is null;
            Customer? previousCustomer = isFirstBid ? null : lot.LastBid!.Customer;

            var bidStatus = customer.TryMakeBid(lot, new(bidInformation.Amount));

            if (bidStatus == BidStatus.Success)
            {
                var newBid = lot.LastBid;
                var result = await bidsRepository.AddAsync(newBid, cancellationToken);
                await lotsRepository.UpdateAsync(lot, cancellationToken);
                if (result is not null)
                {
                    await busControl.Publish(new BidPerLotEvent
                    (
                        customer.Id,
                        previousCustomer is null ? Guid.Empty : previousCustomer.Id,
                        lot.Id,
                        lot.Title.Value,
                        Convert.ToDouble(lot.LastBid!.Amount.Value)
                    )
                    , cancellationToken);
                    if (lot.IsCompleted)
                        await busControl.Publish(new WonLotEvent
                            (
                            customer.Id,
                            lot.Id,
                            lot.Title.Value,
                            Convert.ToDouble(lot.LastBid!.Amount.Value))
                            , cancellationToken);

                    return bidStatus;
                }

                return BidStatus.FaultedIncorrectBid;
            }

            return bidStatus;
        }
    }
}
