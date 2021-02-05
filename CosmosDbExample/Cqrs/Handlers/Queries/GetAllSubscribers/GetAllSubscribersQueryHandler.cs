using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using CosmosDbExample.Infrastructure.Database;
using CosmosDbExample.Infrastructure.Domain.Entities;
using MediatR;

namespace CosmosDbExample.Cqrs.Handlers.Queries.GetAllSubscribers
{
    public class GetAllSubscribersQueryHandler : IRequestHandler<GetAllSubscribersQuery, IEnumerable<Subscribers>>
    {
        private readonly ICosmosDbService FCosmosDbService;

        public GetAllSubscribersQueryHandler(ICosmosDbService ACosmosDbService) 
        {
            FCosmosDbService = ACosmosDbService;
        }

        public async Task<IEnumerable<Subscribers>> Handle(GetAllSubscribersQuery ARequest, CancellationToken ACancellationToken) 
        {
            return await FCosmosDbService.GetItems<Subscribers>($"select * from {typeof(Subscribers).Name}", ACancellationToken);
        } 
    }
}
