using System;
using System.Threading;
using System.Threading.Tasks;
using CosmosDbExample.Database;
using CosmosDbExample.Domain.Entities;
using MediatR;

namespace CosmosDbExample.Cqrs.Handlers.Commands.AddNewArticle
{

    public class AddNewArticleCommandHandler : IRequestHandler<AddNewArticleCommand, Unit>
    {

        private readonly ICosmosDbService FCosmosDbService;

        public AddNewArticleCommandHandler(ICosmosDbService ACosmosDbService)
        {
            FCosmosDbService = ACosmosDbService;
        }

        public async Task<Unit> Handle(AddNewArticleCommand ARequest, CancellationToken ACancellationToken)
        {

            var NewGuid = Guid.NewGuid();
            await FCosmosDbService.AddItem(NewGuid, new Articles
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
