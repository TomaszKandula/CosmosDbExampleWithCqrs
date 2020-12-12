using MediatR;

namespace CosmosDbExample.Cqrs.Handlers.Commands.AddNewSubscriber
{

    public class AddNewSubscriberCommand : IRequest<Unit>
    {
        public string Email { get; set; }
        public bool IsActive { get; set; }
    }

}
