using System;
using MediatR;

namespace CosmosDbExample.Cqrs.Handlers.Commands.AddNewUser
{

    public class AddNewUserCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public string Alias { get; set; }
        public string Email { get; set; }
    }

}
