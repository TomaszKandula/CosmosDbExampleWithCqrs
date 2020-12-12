﻿using System;
using CosmosDbExample.Domain.Entities;
using MediatR;

namespace CosmosDbExample.Cqrs.Handlers.Queries.GetSingleUser
{

    public class GetSingleUserQuery : IRequest<Users>
    {
        public Guid Id { get; set; }
    }

}
