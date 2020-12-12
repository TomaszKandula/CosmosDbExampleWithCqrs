using System.Collections.Generic;
using CosmosDbExample.Domain.Entities;
using MediatR;

namespace CosmosDbExample.Cqrs.Handlers.Queries.GetAllSubscribers
{
    public class GetAllSubscribersQuery : IRequest<IEnumerable<Subscribers>>
    {
    }
}
