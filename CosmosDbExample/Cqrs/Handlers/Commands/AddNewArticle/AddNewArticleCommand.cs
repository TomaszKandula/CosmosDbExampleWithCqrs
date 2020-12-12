using MediatR;
using System;

namespace CosmosDbExample.Cqrs.Handlers.Commands.AddNewArticle
{

    public class AddNewArticleCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public string Status { get; set; }
        public int Likes { get; set; }
        public int ReadCount { get; set; }
    }

}
