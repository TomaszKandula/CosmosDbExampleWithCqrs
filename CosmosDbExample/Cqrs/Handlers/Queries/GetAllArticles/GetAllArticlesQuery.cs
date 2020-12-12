using System.Collections.Generic;
using CosmosDbExample.Domain.Entities;
using MediatR;

namespace CosmosDbExample.Cqrs.Handlers.Queries.GetAllArticles
{
    public class GetAllArticlesQuery : IRequest<IEnumerable<Articles>>
    {
    }
}
