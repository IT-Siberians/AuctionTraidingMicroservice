using Microsoft.Extensions.Logging;
using Otus.QueueDto.User;
using AuctionTrading.Infrastructure.MediatR.Commands;
using MediatR;
using MassTransit;

namespace AuctionTrading.Infrastructure.Queues.Implementations.Consumers
{
    public class CreateUserConsumer(ILogger<CreateUserConsumer> logger, IMediator mediator) : IConsumer<CreateUserEvent>
    {
        public async Task Consume(ConsumeContext<CreateUserEvent> context)
        {
            var result = await mediator.Send(new CreateSellerCommand<CreateUserEvent>(context.Message));

            if (!result)
            {
                logger.LogWarning("Failed to create seller, message will be redelivered.");
                await context.Redeliver(TimeSpan.FromSeconds(10));
            }

            result  = await mediator.Send(new CreateCustomerCommand<CreateUserEvent>(context.Message));

            if (!result)
            {
                logger.LogWarning("Failed to create customer, message will be redelivered.");
                await context.Redeliver(TimeSpan.FromSeconds(10));
            }

        }
    }
}
