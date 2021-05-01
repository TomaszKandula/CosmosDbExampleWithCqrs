using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace CosmosDbExample.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    [ResponseCache(CacheProfileName = "Standard")]
    public class __BaseController : ControllerBase
    {
        protected readonly IMediator FMediator;

        public __BaseController(IMediator AMediator) => FMediator = AMediator;
    }
}
