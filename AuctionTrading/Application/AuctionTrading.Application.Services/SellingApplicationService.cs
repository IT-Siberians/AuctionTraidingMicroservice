using AuctionTrading.Application.Models.Seller;
using AuctionTrading.Application.Services.Abstractions;
using AuctionTrading.Common.Enums;
using AuctionTrading.Domain.Entities;
using AuctionTrading.Domain.Repositories.Abstractions;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionTrading.Application.Services
{
    public class SellingApplicationService(
        ISellersRepository sellersRepository,
        ICustomersRepository customersRepository,
        IAuctionLotRepository lotsRepository,
        IRepository<Bid, Guid> bidsRepository,
        IMapper mapper)
        : ISellingApplicationService
    {
        public async Task<bool> CancelAuctionLotAsync(CancelAuctionLotModel information)
        {
            var seller = await sellersRepository.GetByIdAsync(information.SellerId);
            if (seller is null)
                return false;

            var lot = await lotsRepository.GetByIdAsync(information.AuctionLotId);
            if (lot is null)
                return false;

            await sellersRepository.UpdateAsync(seller);
            return seller.CancelLot(lot);
        }
    }
}
