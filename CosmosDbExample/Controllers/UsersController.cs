using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CosmosDbExample.Shared.Dto;
using CosmosDbExample.Cqrs.Mappers;
using CosmosDbExample.Domain.Entities;
using CosmosDbExample.Cqrs.Handlers.Queries.GetAllUsers;
using CosmosDbExample.Cqrs.Handlers.Queries.GetSingleUser;
using MediatR;

namespace CosmosDbExample.Controllers
{
    public class UsersController : __BaseController
    {
        public UsersController(IMediator AMediator) : base(AMediator)
        {        
        }

        [HttpPost]
        public async Task<Unit> AddNewUser(AddNewUserDto APayLoad) 
        {
            var LCommand = UsersMapper.MapToAddNewUserCommand(APayLoad);
            return await FMediator.Send(LCommand);
        }

        [HttpGet]
        public async Task<IEnumerable<Users>> GetAllUsers() 
        {
            var LQuery = new GetAllUsersQuery();
            return await FMediator.Send(LQuery);
        }

        [HttpGet]
        public async Task<Users> GetSingleUser(Guid Id)
        {
            var LQuery = new GetSingleUserQuery { Id = Id };
            return await FMediator.Send(LQuery);
        }
    }
}
