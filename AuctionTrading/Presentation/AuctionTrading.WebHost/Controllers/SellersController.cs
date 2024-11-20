using AuctionTrading.Application.Models.Seller;
using AuctionTrading.Application.Services.Abstractions;
using AuctionTrading.WebHost.Requests.AuctionLot;
using AuctionTrading.WebHost.Requests.Seller;
using AuctionTrading.WebHost.Responses.AuctionLot;
using AuctionTrading.WebHost.Responses.Customer;
using AuctionTrading.WebHost.Responses.Seller;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AuctionTrading.WebHost.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SellersController(ISellersApplicationService sellersApplicationService,
                                    IAuctionLotsApplicationService auctionLotsApplicationService,
                                    ISellingApplicationService sellingApplicationService,
                                    IMapper mapper) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SellerShortResponse>))]
        public async Task<IActionResult> GetAllSellers(CancellationToken cancellationToken)
        {
            var sellers = await sellersApplicationService.GetSellersAsync(cancellationToken);
            return Ok(mapper.Map<IEnumerable<SellerShortResponse>>(sellers));
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SellerDetailedResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Guid))]
        public async Task<IActionResult> GetSellerById(Guid id, CancellationToken cancellationToken)
        {
            var seller = await sellersApplicationService.GetSellerByIdAsync(id, cancellationToken);
            if (seller is null)
                return NotFound($"Seller with id:{id} not found");
            return Ok(mapper.Map<SellerDetailedResponse>(seller));
        }

        [HttpGet("{username}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SellerDetailedResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Guid))]
        public async Task<IActionResult> GetSellerByUsernameId(string username, CancellationToken cancellationToken)
        {
            var seller = await sellersApplicationService.GetSellerByUsernameAsync(username, cancellationToken);
            if (seller is null)
                return NotFound($"Seller with username:{username} not found");
            return Ok(mapper.Map<SellerDetailedResponse>(seller));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SellerShortResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Guid))]
        public async Task<IActionResult> CreateSeller(CreateSellerRequest request, CancellationToken cancellationToken)
        {
            var seller = mapper.Map<CreateSellerModel>(request);
            var createdSeller = await sellersApplicationService.CreateSellerAsync(seller, cancellationToken);
            if (createdSeller is null)
                return BadRequest("Seller can not be created");

            var sellerResponse = mapper.Map<CustomerShortResponse>(createdSeller);
            return CreatedAtAction(nameof(GetSellerById), new { sellerResponse.Id }, sellerResponse);
        }

        [HttpPost("cancel auction lot")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AuctionLotDetailedResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Guid))]
        public async Task<ActionResult<AuctionLotDetailedResponse>> CancelAuctionLotAsync(CancelAuctionLotRequest request, CancellationToken cancellationToken)
        {
            var auctionLot = await auctionLotsApplicationService.GetAuctionLotByIdAsync(request.AuctionLotId, cancellationToken);
            if (auctionLot is null)
                return NotFound($"Auction lot with id:{request.AuctionLotId} not found");

            var seller = await sellersApplicationService.GetSellerByIdAsync(request.SellerId, cancellationToken);
            if (seller is null)
                return NotFound($"Seller with id:{request.SellerId} not found");

            if (seller.AuctionedLots is null || seller.AuctionedLots.FirstOrDefault(l => l.Id == request.AuctionLotId) is null)
                return BadRequest($"Lot with id: {request.AuctionLotId} is not owned by the seller with id: {request.SellerId}");

            return await sellingApplicationService.CancelAuctionLotAsync(auctionLot, cancellationToken)
                ? Created("", mapper.Map<AuctionLotShortResponse>(auctionLot))
                : BadRequest($"Seller has not cancel this auction lot with id {request.AuctionLotId} ");
        }
    }
}