using CosmosDbExample.Cqrs.Handlers.Commands.AddNewUser;
using CosmosDbExample.Shared.Dto;

namespace CosmosDbExample.Cqrs.Mappers
{

    public static class UsersMapper
    {

        public static AddNewUserCommand MapToAddNewUserCommand(AddNewUserDto AModel) 
        {
            return new AddNewUserCommand 
            { 
                Id = AModel.Id,
                Alias = AModel.Alias,
                Email = AModel.Email
            };
        }

    }

}
