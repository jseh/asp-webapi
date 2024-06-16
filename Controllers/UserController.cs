using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi_JWT.Utils;

namespace WebApi_JWT.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        [Authorize(Policy = "CanAccessVIPArea")]
        [Authorize(Policy = "OnlyAdmin")]
        [HttpGet(Name = "GetUsersList")]
        public IEnumerable<int> Get()
        {
            var s = this.User;
            return [7, 9, 0];
        }
        
        [HttpPost("rds/set")]
        public string GuardarEnRedis()
        {
            

            var d = RedisCon.Connection.GetDatabase();

            d.StringSet("85070591730234615847396907784232501249", "fg45562fe");

            return "todo bien...";
        }
        
        [HttpGet("rds/get")]
        public string RecuperarEnRedis()
        {

            var d = RedisCon.Connection.GetDatabase();

            return d.StringGet("85070591730234615847396907784232501249");

        }
    }
}
