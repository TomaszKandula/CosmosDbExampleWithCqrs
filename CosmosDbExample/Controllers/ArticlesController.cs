using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CosmosDbExample.Shared.Dto;
using CosmosDbExample.Cqrs.Mappers;
using CosmosDbExample.Infrastructure.Domain.Entities;
using CosmosDbExample.Cqrs.Handlers.Queries.GetAllArticles;
using CosmosDbExample.Cqrs.Handlers.Queries.GetSingleArticle;
using MediatR;

namespace CosmosDbExample.Controllers
{
    public class ArticlesController : __BaseController
    {
        public ArticlesController(IMediator AMediator) : base(AMediator) { }

        [HttpPost]
        public async Task<Unit> AddNewArticle(AddNewArticleDto APayLoad)
            => await FMediator.Send(ArticlesMapper.MapToAddNewArticleCommand(APayLoad));

        [HttpGet]
        public async Task<IEnumerable<Articles>> GetAllArticles()
            => await FMediator.Send(new GetAllArticlesQuery());

        [HttpGet]
        public async Task<Articles> GetSingleArticle(Guid Id)
            => await FMediator.Send(new GetSingleArticleQuery { Id = Id });
    }
}
