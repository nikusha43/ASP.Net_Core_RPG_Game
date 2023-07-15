using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetRPG.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]

    [ApiVersion("2.0")] 

    public class TestController : ControllerBase
    {
 

        [HttpGet]
        [MapToApiVersion("2.0")]
        public string GetV2()
        {
            return "Its V2";
        }
    }
}
