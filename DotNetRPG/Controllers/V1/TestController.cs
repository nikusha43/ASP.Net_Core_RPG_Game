using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetRPG.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0", Deprecated = true)]


    public class TestController : ControllerBase
    {
        [HttpGet]
        [MapToApiVersion("1.0")]
        public string GetV1()
        {
            return "Its V1";
        }

       
    }
}
