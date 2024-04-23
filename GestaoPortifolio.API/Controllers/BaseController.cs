using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GestaoPortifolio.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public abstract class BaseController : ControllerBase
    {
        protected int? userId;

        protected BaseController(IHttpContextAccessor httpContextAccessor) 
        {
            string t = httpContextAccessor ?.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Sid)?.Value;
            if(!string.IsNullOrEmpty(t))
            {
                userId = Convert.ToInt32(t);
            }
        }
    }
}
