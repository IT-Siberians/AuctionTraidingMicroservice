using AuctionTrading.Application.Models.AuctionLot;
using AuctionTrading.Application.Models.Seller;
using AuctionTrading.Application.Services.Abstractions;
using AuctionTrading.Domain.Entities;
using AuctionTrading.Domain.Repositories.Abstractions;
using AutoMapper;
using MassTransit;

namespace AuctionTrading.Application.Services
{
    public class SellingApplicationService(ISellersRepository sellersRepository, IAuctionLotRepository lotsRepository, IBusControl busControl)
        : ISellingApplicationService
    {
        public async Task<bool> CancelAuctionLotAsync(AuctionLotModel information, CancellationToken cancellationToken = default)
        {
            var seller = await sellersRepository.GetByIdAsync(information.SellerId, cancellationToken);
            if (seller is null)
                return false;

            var lot = await lotsRepository.GetByIdAsync(information.Id, cancellationToken);
            if (lot is null)
                return false;

            if (!seller.CancelLot(lot))
                return false;

            if (!await sellersRepository.UpdateAsync(seller, cancellationToken))
                return false;

            //await _busControl.Publish(new MessageDto
            //{
            //    Content = $"Lesson {createdLesson.Id} with subject {createdLesson.Subject} is added"
            //});
            return true;
        }
    }
}
