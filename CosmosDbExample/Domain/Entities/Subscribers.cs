using System;

namespace CosmosDbExample.Domain.Entities
{
    public class Subscribers : __EntityKey
    {
        public string Email { get; set; }
        public string Status { get; set; }
        public int Count { get; set; }
        public DateTime Registered { get; set; }
        public DateTime? LastUpdated { get; set; }
    }
}
