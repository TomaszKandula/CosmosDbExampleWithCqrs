using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CosmosDbExample.Shared.Dto;
using CosmosDbExample.Cqrs.Mappers;
using CosmosDbExample.Infrastructure.Domain.Entities;
using CosmosDbExample.Cqrs.Handlers.Queries.GetAllUsers;
using CosmosDbExample.Cqrs.Handlers.Queries.GetSingleUser;
using MediatR;

namespace CosmosDbExample.Controllers
{
    public class UsersController : __BaseController
    {
        public UsersController(IMediator AMediator) : base(AMediator) { }

        [HttpPost]
        public async Task<Unit> AddNewUser(AddNewUserDto APayLoad) 
            => await FMediator.Send(UsersMapper.MapToAddNewUserCommand(APayLoad));

        [HttpGet]
        public async Task<IEnumerable<Users>> GetAllUsers() 
            => await FMediator.Send(new GetAllUsersQuery());

        [HttpGet]
        public async Task<Users> GetSingleUser(Guid Id)
            => await FMediator.Send(new GetSingleUserQuery { Id = Id });
    }
}
