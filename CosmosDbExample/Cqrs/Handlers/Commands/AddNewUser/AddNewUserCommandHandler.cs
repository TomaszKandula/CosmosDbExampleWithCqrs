using System;
using System.Threading;
using System.Threading.Tasks;
using CosmosDbExample.Infrastructure.Database;
using CosmosDbExample.Infrastructure.Domain.Entities;
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
            await FCosmosDbService.AddItem(NewGuid, new Users 
            { 
                Id = NewGuid,
                UserAlias = ARequest.Alias,
                EmailAddress = ARequest.Email
            }, ACancellationToken);

            return await Task.FromResult(Unit.Value);
        }
    }
}
