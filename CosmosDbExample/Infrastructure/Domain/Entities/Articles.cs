namespace CosmosDbExample.Infrastructure.Domain.Entities
{  
    public class Articles : EntityKey
    {
        public string Title { get; set; }
        public string Desc { get; set; }
        public string Status { get; set; }
        public int Likes { get; set; }
        public int ReadCount { get; set; }
    }
}
