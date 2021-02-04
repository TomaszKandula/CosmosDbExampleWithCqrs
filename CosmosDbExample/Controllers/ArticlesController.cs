using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CosmosDbExample.Shared.Dto;
using CosmosDbExample.Cqrs.Mappers;
using CosmosDbExample.Domain.Entities;
using CosmosDbExample.Cqrs.Handlers.Queries.GetAllArticles;
using CosmosDbExample.Cqrs.Handlers.Queries.GetSingleArticle;
using MediatR;

namespace CosmosDbExample.Controllers
{
    public class ArticlesController : __BaseController
    {
        public ArticlesController(IMediator AMediator) : base(AMediator) 
        { 
        }

        [HttpPost]
        public async Task<Unit> AddNewArticle(AddNewArticleDto APayLoad)
        {
            var LCommand = ArticlesMapper.MapToAddNewArticleCommand(APayLoad);
            return await FMediator.Send(LCommand);
        }

        [HttpGet]
        public async Task<IEnumerable<Articles>> GetAllArticles()
        {
            var LQuery = new GetAllArticlesQuery();
            return await FMediator.Send(LQuery);
        }

        [HttpGet]
        public async Task<Articles> GetSingleArticle(Guid Id)
        {
            var LQuery = new GetSingleArticleQuery { Id = Id };
            return await FMediator.Send(LQuery);
        }
    }
}
