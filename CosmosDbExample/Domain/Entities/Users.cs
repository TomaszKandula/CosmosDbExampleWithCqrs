using System;

namespace CosmosDbExample.Domain.Entities
{

    public class Users
    {
        public string Id { get; set; }
        public string UserAlias { get; set; }
        public string UserStatus { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public DateTime Registered { get; set; }
        public DateTime LastLogged { get; set; }
        public DateTime LastUpdated { get; set; }
    }

}
