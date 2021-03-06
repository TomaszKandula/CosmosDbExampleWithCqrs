﻿using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CosmosDbExample.Shared.Dto;
using CosmosDbExample.Cqrs.Mappers;
using CosmosDbExample.Infrastructure.Domain.Entities;
using CosmosDbExample.Cqrs.Handlers.Queries.GetAllSubscribers;
using CosmosDbExample.Cqrs.Handlers.Queries.GetSingleSubscriber;
using MediatR;

namespace CosmosDbExample.Controllers
{
    public class SubscribersController : __BaseController
    {
        public SubscribersController(IMediator AMediator) : base(AMediator) { }

        [HttpPost]
        public async Task<Unit> AddNewSubscriber(AddNewSubscriberDto APayLoad)
            => await FMediator.Send(SubscribersMapper.MapToAddNewSubscriberCommand(APayLoad));

        [HttpGet]
        public async Task<IEnumerable<Subscribers>> GetAllSubscribers()
            => await FMediator.Send(new GetAllSubscribersQuery());

        [HttpGet]
        public async Task<Subscribers> GetSingleSubscriber(Guid Id)
            => await FMediator.Send(new GetSingleSubscriberQuery { Id = Id });
    }
}
