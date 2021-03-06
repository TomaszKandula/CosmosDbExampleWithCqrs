﻿using System;
using System.Threading;
using System.Threading.Tasks;
using CosmosDbExample.Infrastructure.Database;
using CosmosDbExample.Infrastructure.Domain.Entities;
using MediatR;

namespace CosmosDbExample.Cqrs.Handlers.Commands.AddNewArticle
{
    public class AddNewArticleCommandHandler : IRequestHandler<AddNewArticleCommand, Unit>
    {
        private readonly ICosmosDbService FCosmosDbService;

        public AddNewArticleCommandHandler(ICosmosDbService ACosmosDbService)
            => FCosmosDbService = ACosmosDbService;

        public async Task<Unit> Handle(AddNewArticleCommand ARequest, CancellationToken ACancellationToken)
        {
            var LNewGuid = Guid.NewGuid();

            await FCosmosDbService.AddItem(LNewGuid, new Articles
            {
                Id = ARequest.Id,
                Title = ARequest.Title,
                Desc = ARequest.Desc,
                Status = ARequest.Status,
                Likes = ARequest.Likes,
                ReadCount = ARequest.ReadCount
            }, ACancellationToken);

            return await Task.FromResult(Unit.Value);
        }
    }
}
