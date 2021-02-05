using System;
using CosmosDbExample.Infrastructure.Domain.Entities;
using MediatR;

namespace CosmosDbExample.Cqrs.Handlers.Queries.GetSingleSubscriber
{
    public class GetSingleSubscriberQuery : IRequest<Subscribers>
    {
        public Guid Id { get; set; }
    }
}
