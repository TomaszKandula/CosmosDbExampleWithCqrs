using CosmosDbExample.Cqrs.Handlers.Commands.AddNewArticle;
using CosmosDbExample.Shared.Dto;

namespace CosmosDbExample.Cqrs.Mappers
{
    public static class ArticlesMapper
    {
        public static AddNewArticleCommand MapToAddNewArticleCommand(AddNewArticleDto AModel) 
        {
            return new AddNewArticleCommand
            {
                Id = AModel.Id,
                Title = AModel.Title,
                Desc = AModel.Desc,
                Status = AModel.Status,
                Likes = AModel.Likes,
                ReadCount = AModel.ReadCount
            };        
        }
    }
}
