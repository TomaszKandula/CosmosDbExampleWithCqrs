using System;
using CosmosDbExample.Infrastructure.Domain.Entities;
using MediatR;

namespace CosmosDbExample.Cqrs.Handlers.Queries.GetSingleArticle
{
    public class GetSingleArticleQuery : IRequest<Articles>
    {
        public Guid Id { get; set; }
    }
}
