using AuctionTrading.Application.Models.AuctionLot;
using AuctionTrading.Application.Models.Bid;
using AuctionTrading.Application.Models.Customer;
using AuctionTrading.Application.Models.Seller;
using AuctionTrading.Domain.Entities;
using AutoMapper;

namespace AuctionTrading.Application.Services.Mapping
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<AuctionLot, AuctionLotModel>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title.Value))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description.Value))
                .ForMember(dest => dest.StartPrice, opt => opt.MapFrom(src => src.StartPrice.Value))
                .ForMember(dest => dest.BidIncrement, opt => opt.MapFrom(src => src.BidIncrement.Value))
                .ForMember(dest => dest.RepurchasePrice, opt => opt.MapFrom(src => src.RepurchasePrice.Value));

            CreateMap<Bid, BidModel>()
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount.Value));

            CreateMap<Customer, CustomerModel>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username.Value));

            CreateMap<Seller, SellerModel>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username.Value));
        }
    }
}
