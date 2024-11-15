using MassTransit.Mediator;
using MassTransit;
using Microsoft.Extensions.Logging;
using Otus.QueueDto.User;
using AuctionTrading.Infrastructure.MediatR.Commands;

namespace AuctionTrading.Infrastructure.Queues.Implementations.Consumers
{
    public class CreateUserConsumer(ILogger<CreateUserConsumer> logger, IMediator mediator) : IConsumer<CreateUserEvent>
    {
        public async Task Consume(ConsumeContext<CreateUserEvent> context)
        {
            await mediator.Send(new CreateSellerCommand<CreateUserEvent>(context.Message));

            //if (!result)
            //{
            //    logger.LogWarning("Failed to create user, message will be redelivered.");
            //    await context.Redeliver(TimeSpan.FromSeconds(10));
            //}
        }
    }
}
