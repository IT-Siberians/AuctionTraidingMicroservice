
using AuctionTrading.Application.Models.AuctionLot;
using AuctionTrading.Application.Services.Abstractions;
using AuctionTrading.WebHost.Requests.AuctionLot;
using AuctionTrading.WebHost.Responses.AuctionLot;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AuctionTrading.WebHost.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuctionLotsController(IAuctionLotsApplicationService auctionLotsApplicationService,
                                    IMapper mapper) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AuctionLotShortResponse>))]
        public async Task<IActionResult> GetAllAuctionLots(CancellationToken cancellationToken)
        {
            var auctionLots = await auctionLotsApplicationService.GetAuctionLotsAsync(cancellationToken);
            return Ok(mapper.Map<IEnumerable<AuctionLotShortResponse>>(auctionLots));
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuctionLotDetailedResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> GetAuctionLotById(Guid id, CancellationToken cancellationToken)
        {
            var auctionLot = await auctionLotsApplicationService.GetAuctionLotByIdAsync(id, cancellationToken);
            if (auctionLot is null)
                return NotFound($"Auction lot with id:{id} not found");
            return Ok(mapper.Map<AuctionLotDetailedResponse>(auctionLot));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AuctionLotShortResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CreateAuctionLot(CreateAuctionLotRequest request, CancellationToken cancellationToken)
        {
            var auctionLot = mapper.Map<CreateAuctionLotModel>(request);
            var createdAuctionLot = await auctionLotsApplicationService.CreateAuctionLotAsync(auctionLot, cancellationToken);
            if (createdAuctionLot is null)
                return BadRequest("Auction lot can not be created");

            var auctionLotResponse = mapper.Map<AuctionLotShortResponse>(createdAuctionLot);
            return CreatedAtAction(nameof(GetAuctionLotById), new { auctionLotResponse.Id }, auctionLotResponse);
        }
    }
}
