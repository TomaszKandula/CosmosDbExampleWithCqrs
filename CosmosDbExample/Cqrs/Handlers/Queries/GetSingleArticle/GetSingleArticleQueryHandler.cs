using System.Threading;
using System.Threading.Tasks;
using CosmosDbExample.Database;
using CosmosDbExample.Domain.Entities;
using MediatR;

namespace CosmosDbExample.Cqrs.Handlers.Queries.GetSingleArticle
{

    public class GetSingleArticleQueryHandler : IRequestHandler<GetSingleArticleQuery, Articles>
    {

        private readonly ICosmosDbService FCosmosDbService;

        public GetSingleArticleQueryHandler(ICosmosDbService ACosmosDbService) 
        {
            FCosmosDbService = ACosmosDbService;
        }

        public async Task<Articles> Handle(GetSingleArticleQuery ARequest, CancellationToken ACancellationToken)
        {
            FCosmosDbService.InitContainer<Articles>();
            return await FCosmosDbService.GetItem<Articles>(ARequest.Id);
        }
    }

}
