using CosmosDbExample.Cqrs.Handlers.Commands.AddNewSubscriber;
using CosmosDbExample.Shared.Dto;

namespace CosmosDbExample.Cqrs.Mappers
{
    public static class SubscribersMapper
    {
        public static AddNewSubscriberCommand MapToAddNewSubscriberCommand(AddNewSubscriberDto AModel) 
        {
            return new AddNewSubscriberCommand 
            { 
                Email = AModel.Email,
                IsActive = AModel.IsActive
            };
        }
    }
}
