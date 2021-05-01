using System;

namespace CosmosDbExample.Shared.Dto
{
    public class AddNewUserDto
    {
        public Guid Id { get; set; }
        
        public string Alias { get; set; }

        public string Email { get; set; }
    }
}
