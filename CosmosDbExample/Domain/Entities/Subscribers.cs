﻿using System;

namespace CosmosDbExample.Domain.Entities
{
    public class Subscribers
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public int Count { get; set; }
        public DateTime Registered { get; set; }
        public DateTime? LastUpdated { get; set; }
    }
}
