using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using CosmosDbExample.Database;
using CosmosDbExample.Domain.Entities;
using MediatR;

namespace CosmosDbExample.Cqrs.Handlers.Queries.GetAllArticles
{

    public class GetAllArticlesQueryHandler : IRequestHandler<GetAllArticlesQuery, IEnumerable<Articles>>
    {

        private readonly ICosmosDbService FCosmosDbService;

        public GetAllArticlesQueryHandler(ICosmosDbService ACosmosDbService) 
        {
            FCosmosDbService = ACosmosDbService;
        }

        public async Task<IEnumerable<Articles>> Handle(GetAllArticlesQuery ARequest, CancellationToken ACancellationToken)
        {
            return await FCosmosDbService.GetItems<Articles>($"select * from {typeof(Articles).Name}");
        }
    }

}
