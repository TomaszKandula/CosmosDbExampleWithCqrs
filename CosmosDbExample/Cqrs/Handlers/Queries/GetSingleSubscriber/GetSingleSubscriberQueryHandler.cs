using System.Threading;
using System.Threading.Tasks;
using CosmosDbExample.Infrastructure.Database;
using CosmosDbExample.Infrastructure.Domain.Entities;
using MediatR;

namespace CosmosDbExample.Cqrs.Handlers.Queries.GetSingleSubscriber
{
    public class GetSingleSubscriberQueryHandler : IRequestHandler<GetSingleSubscriberQuery, Subscribers>
    {

        private readonly ICosmosDbService FCosmosDbService;

        public GetSingleSubscriberQueryHandler(ICosmosDbService ACosmosDbService)
        {
            FCosmosDbService = ACosmosDbService;
        }

        public async Task<Subscribers> Handle(GetSingleSubscriberQuery ARequest, CancellationToken ACancellationToken)
        {
            return await FCosmosDbService.GetItem<Subscribers>(ARequest.Id, ACancellationToken);
        }
    }
}
