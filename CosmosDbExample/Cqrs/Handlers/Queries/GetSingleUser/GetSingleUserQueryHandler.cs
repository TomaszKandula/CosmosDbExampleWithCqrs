using System.Threading;
using System.Threading.Tasks;
using CosmosDbExample.Database;
using CosmosDbExample.Domain.Entities;
using MediatR;

namespace CosmosDbExample.Cqrs.Handlers.Queries.GetSingleUser
{

    public class GetSingleUserQueryHandler : IRequestHandler<GetSingleUserQuery, Users>
    {

        private readonly ICosmosDbService FCosmosDbService;

        public GetSingleUserQueryHandler(ICosmosDbService ACosmosDbService)
        {
            FCosmosDbService = ACosmosDbService;
        }

        public async Task<Users> Handle(GetSingleUserQuery ARequest, CancellationToken ACancellationToken)
        {
            return await FCosmosDbService.GetItem<Users>(ARequest.Id);
        }

    }

}
