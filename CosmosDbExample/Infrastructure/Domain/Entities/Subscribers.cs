using System;

namespace CosmosDbExample.Infrastructure.Domain.Entities
{
    public class Subscribers : EntityKey
    {
        public string Email { get; set; }

        public string Status { get; set; }
        
        public int Count { get; set; }
        
        public DateTime Registered { get; set; }
        
        public DateTime? LastUpdated { get; set; }
    }
}
