using System.Collections.Generic;
using CosmosDbExample.Domain.Entities;
using MediatR;

namespace CosmosDbExample.Cqrs.Handlers.Queries.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<IEnumerable<Users>>
    {
    }
}
