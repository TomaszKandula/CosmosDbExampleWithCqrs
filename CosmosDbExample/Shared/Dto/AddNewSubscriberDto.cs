using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CosmosDbExample.Shared.Dto
{

    public class AddNewSubscriberDto
    {
        public string Email { get; set; }
        public bool IsActive { get; set; }
    }

}
