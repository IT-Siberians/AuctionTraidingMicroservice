using AuctionTrading.Application.Models.Bid;
using AuctionTrading.Application.Models.Customer;
using AuctionTrading.Application.Services.Abstractions;
using AuctionTrading.WebHost.Requests.Bid;
using AuctionTrading.WebHost.Requests.Customer;
using AuctionTrading.WebHost.Responses.AuctionLot;
using AuctionTrading.WebHost.Responses.Bid;
using AuctionTrading.WebHost.Responses.Customer;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AuctionTrading.WebHost.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CustomersController(ICustomersApplicationService customersApplicationService,
                                    IAuctionLotsApplicationService auctionLotsApplicationService,
                                    IBidderApplicationService bidderApplicationService,
                                    IMapper mapper) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CustomerShortResponse>))]
        public async Task<IActionResult> GetAllCustomers(CancellationToken cancellationToken)
        {
            var customers = await customersApplicationService.GetCustomersAsync(cancellationToken);
            return Ok(mapper.Map<IEnumerable<CustomerShortResponse>>(customers));
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomerDetailedResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> GetCustomerById(Guid id, CancellationToken cancellationToken)
        {
            var customer = await customersApplicationService.GetCustomerByIdAsync(id, cancellationToken);
            if (customer is null)
                return NotFound($"Customer with id:{id} not found");
            return Ok(mapper.Map<CustomerDetailedResponse>(customer));
        }

        [HttpGet("{username}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomerDetailedResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> GetCustomerByUsernameId(string username, CancellationToken cancellationToken)
        {
            var customer = await customersApplicationService.GetCustomerByUsernameAsync(username, cancellationToken);
            if (customer is null)
                return NotFound($"Customer with username:{username} not found");
            return Ok(mapper.Map<CustomerDetailedResponse>(customer));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CustomerShortResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> CreateCustomer(CreateCustomerRequest request, CancellationToken cancellationToken)
        {
            var customer = mapper.Map<CreateCustomerModel>(request);

            var createdCustomer = await customersApplicationService.CreateCustomerAsync(customer, cancellationToken);
            if (createdCustomer is null)
                return BadRequest("Customer can not be created");

            var customerResponse = mapper.Map<CustomerShortResponse>(createdCustomer);
            return CreatedAtAction(nameof(GetCustomerById), new { customerResponse.Id }, customerResponse);
        }

        [HttpPost("Add bid")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AuctionLotDetailedResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Guid))]
        public async Task<IActionResult> TryBidOnAuctionLot(CreateBidRequest request, CancellationToken cancellationToken)
        {
            var customer = await customersApplicationService.GetCustomerByIdAsync(request.CustomerId, cancellationToken);
            var auctionLot = await auctionLotsApplicationService.GetAuctionLotByIdAsync(request.AuctionLotId, cancellationToken);

            if (auctionLot is null)
                return NotFound($"Auction lot with id:{request.AuctionLotId} not found");

            if (customer is null)
                return NotFound($"Customer with id:{request.CustomerId} not found");


            if (auctionLot.SellerId == customer.Id)
                return BadRequest($"The buyer cannot bid on his auction lot with id {auctionLot.Id}");

            var bid = mapper.Map<CreateBidModel>(request);
            var response = await bidderApplicationService.MakeBidAsync(bid, cancellationToken);
            if (response.Result != Common.Enums.BidStatus.Success)
                return response.Message is not null
                    ? BadRequest(response.Message)
                    : BadRequest($"An attempt to place a bet ended in failure. Status {response}");
            
            auctionLot = await auctionLotsApplicationService.GetAuctionLotByIdAsync(request.AuctionLotId, cancellationToken);
            return Created("", mapper.Map<BidDetailedResponse>(auctionLot.LastBid));

        }
    }
}
