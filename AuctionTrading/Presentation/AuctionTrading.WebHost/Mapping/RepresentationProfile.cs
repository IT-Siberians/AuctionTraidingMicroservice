using AuctionTrading.Application.Models.AuctionLot;
using AuctionTrading.Application.Models.Bid;
using AuctionTrading.Application.Models.Customer;
using AuctionTrading.Application.Models.Seller;
using AuctionTrading.WebHost.Requests.AuctionLot;
using AuctionTrading.WebHost.Requests.Bid;
using AuctionTrading.WebHost.Requests.Customer;
using AuctionTrading.WebHost.Requests.Seller;
using AuctionTrading.WebHost.Responses.AuctionLot;
using AuctionTrading.WebHost.Responses.Bid;
using AuctionTrading.WebHost.Responses.Customer;
using AuctionTrading.WebHost.Responses.Seller;
using AutoMapper;

namespace GradeBookMicroservice.WebHost.Mapping
{
    public class RepresentationProfile : Profile
    {
        public RepresentationProfile()
        {
            CreateMap<SellerModel, SellerShortResponse>();
            CreateMap<SellerModel, SellerDetailedResponse>();
            CreateMap<CreateSellerRequest, CreateSellerModel>();
            CreateMap<CreateSellerModel, SellerShortResponse>();

            CreateMap<CustomerModel, CustomerShortResponse>();
            CreateMap<CustomerModel, CustomerDetailedResponse>();
            CreateMap<CreateCustomerRequest, CreateCustomerModel>();
            CreateMap<CreateCustomerModel, CustomerShortResponse>();

            CreateMap<AuctionLotModel, AuctionLotShortResponse>();
            CreateMap<AuctionLotModel, AuctionLotDetailedResponse>();
            CreateMap<CreateAuctionLotRequest, CreateAuctionLotModel>();
            CreateMap<CreateAuctionLotModel, AuctionLotShortResponse>();

            CreateMap<BidModel, BidDetailedResponse>();
            CreateMap<CreateBidRequest, CreateBidModel>();
            CreateMap<CreateBidModel, BidDetailedResponse>();
        }

    }
}