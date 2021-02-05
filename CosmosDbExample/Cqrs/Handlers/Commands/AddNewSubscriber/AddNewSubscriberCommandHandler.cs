using System;
using System.Threading;
using System.Threading.Tasks;
using CosmosDbExample.Infrastructure.Database;
using CosmosDbExample.Infrastructure.Domain.Entities;
using MediatR;

namespace CosmosDbExample.Cqrs.Handlers.Commands.AddNewSubscriber
{
    public class AddNewSubscriberCommandHandler : IRequestHandler<AddNewSubscriberCommand, Unit>
    {
        private readonly ICosmosDbService FCosmosDbService;

        public AddNewSubscriberCommandHandler(ICosmosDbService ACosmosDbService)
        {
            FCosmosDbService = ACosmosDbService;
        }

        public async Task<Unit> Handle(AddNewSubscriberCommand ARequest, CancellationToken ACancellationToken)
        {
            var NewGuid = Guid.NewGuid();
            await FCosmosDbService.AddItem(NewGuid, new Subscribers
            {
                Id = NewGuid,
                Email = ARequest.Email
            }, ACancellationToken);

            return await Task.FromResult(Unit.Value);
        }
    }
}
