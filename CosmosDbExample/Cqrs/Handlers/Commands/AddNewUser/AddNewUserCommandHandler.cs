﻿using System;
using System.Threading;
using System.Threading.Tasks;
using CosmosDbExample.Database;
using CosmosDbExample.Domain.Entities;
using MediatR;

namespace CosmosDbExample.Cqrs.Handlers.Commands.AddNewUser
{

    public class AddNewUserCommandHandler : IRequestHandler<AddNewUserCommand, Unit>
    {

        private readonly ICosmosDbService FCosmosDbService;

        public AddNewUserCommandHandler(ICosmosDbService ACosmosDbService) 
        {
            FCosmosDbService = ACosmosDbService;
        }

        public async Task<Unit> Handle(AddNewUserCommand ARequest, CancellationToken ACancellationToken) 
        {

            var NewGuid = Guid.NewGuid();
            FCosmosDbService.InitContainer<Users>();
            await FCosmosDbService.AddItem(NewGuid, new Users 
            { 
                Id = NewGuid.ToString(),
                UserAlias = ARequest.Alias,
                EmailAddress = ARequest.Email
            });

            return await Task.FromResult(Unit.Value);

        }

    }

}
