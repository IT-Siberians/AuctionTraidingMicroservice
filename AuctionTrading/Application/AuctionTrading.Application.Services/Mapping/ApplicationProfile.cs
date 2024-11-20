using AuctionTrading.Application.Models.AuctionLot;
using AuctionTrading.Application.Models.Bid;
using AuctionTrading.Application.Models.Customer;
using AuctionTrading.Application.Models.Seller;
using AuctionTrading.Domain.Entities;
using AuctionTrading.Domain.ValueObjects;
using AutoMapper;

namespace AuctionTrading.Application.Services.Mapping
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<Money, decimal>().ConvertUsing(x => x.Value);
            CreateMap<AuctionLot, AuctionLotModel>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title.Value))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description.Value))
            .ForMember(dest => dest.StartPrice, opt => opt.MapFrom(src => src.StartPrice))
            .ForMember(dest => dest.BidIncrement, opt => opt.MapFrom(src => src.BidIncrement))
            .ForMember(dest => dest.RepurchasePrice, opt => opt.MapFrom((opt, dest) => opt.RepurchasePrice?.Value ?? null))
            .ForMember(dest => dest.LastBid, opt => opt.MapFrom((opt, dest) => opt.LastBid ?? null))
            .ForMember(dest => dest.SellerId, opt => opt.MapFrom(src => src.Seller.Id));

            CreateMap<Bid, BidModel>()
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount.Value));

            CreateMap<Customer, CustomerModel>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username.Value))
                .ForMember(dest => dest.ObservedAuctionLots, opt => opt.MapFrom(src => src.ObservableAuctionLots));

            CreateMap<Seller, SellerModel>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username.Value))
                .ForMember(dest => dest.AuctionedLots, opt => opt.MapFrom(src => src.ActiveAuctionLots));
        }
    }
}
