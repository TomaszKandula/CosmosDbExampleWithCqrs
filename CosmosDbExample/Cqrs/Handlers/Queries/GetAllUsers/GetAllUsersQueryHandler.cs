using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using CosmosDbExample.Database;
using CosmosDbExample.Domain.Entities;
using MediatR;

namespace CosmosDbExample.Cqrs.Handlers.Queries.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<Users>>
    {

        private readonly ICosmosDbService FCosmosDbService;

        public GetAllUsersQueryHandler(ICosmosDbService ACosmosDbService) 
        {
            FCosmosDbService = ACosmosDbService;
        }

        public async Task<IEnumerable<Users>> Handle(GetAllUsersQuery ARequest, CancellationToken ACancellationToken) 
        {
            return await FCosmosDbService.GetItems<Users>($"select * from {typeof(Users).Name}");
        }

    }

}
