using AuctionGrpcClient;
using AuctionTrading.Application.Models.Bid;
using AuctionTrading.Application.Models.Seller;
using AuctionTrading.Application.Services.Abstractions;
using AuctionTrading.Common.Enums;
using AuctionTrading.Common.Infrastructure.Queues.Abstraction;
using AuctionTrading.Domain.Entities;
using AuctionTrading.Domain.Repositories.Abstractions;
using AuctionTrading.Domain.ValueObjects;
using AuctionTrading.GrpcClient;
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
        IProducerService<BidPerLotEvent> lotBidProducer,
        IProducerService<WonLotEvent> lotPurchasedProducer
        ITradingClient client)
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
                var request = new ReserveMoneyCommandGrpc
                {
                    BuyerId = customer.Id.ToString(),
                    Price = (double)bidInformation.Amount,
                    Lot = new LotInfoModelGrpc
                    {
                        Id = lot.Id.ToString(),
                        Title = lot.Title.Value,
                        Description = lot.Description.Value
                    }
                };

                var response = await client.ReserveMoney(request, cancellationToken);
                if (response.IsError == true)
                    return BidStatus.FaultedIncorrectBid;

                var newBid = lot.LastBid;
                var result = await bidsRepository.AddAsync(newBid, cancellationToken);
                if (result is not null)
                {
                    lotBidProducer.Send(new BidPerLotEvent
                    (
                        customer.Id,
                        previousCustomer is null ? Guid.Empty : previousCustomer.Id,
                        lot.Seller.Id,
                        lot.Id,
                        lot.Title.Value,
                        lot.LastBid!.Amount.Value
                    ));
                    if (lot.IsCompleted)
                        lotPurchasedProducer.Send(new WonLotEvent
                            (
                            customer.Id,
                            lot.Seller.Id,
                            lot.Id,
                            lot.LastBid!.Amount.Value,
                            lot.Title.Value,
                            lot.Description.Value,
                            lot.StartPrice.Value,
                            lot.BidIncrement.Value,
                            lot.RepurchasePrice.Value,
                            lot.StartDate,
                            lot.EndDate
                            ));

                    return bidStatus;
                }

                return BidStatus.FaultedIncorrectBid;
            }

            return bidStatus;
        }
    }
}
