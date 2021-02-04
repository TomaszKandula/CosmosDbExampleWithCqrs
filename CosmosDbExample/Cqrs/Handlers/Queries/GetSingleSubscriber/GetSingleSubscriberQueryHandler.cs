using System.Threading;
using System.Threading.Tasks;
using CosmosDbExample.Database;
using CosmosDbExample.Domain.Entities;
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
