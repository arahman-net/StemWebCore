using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SharedStem.Core.Entities;
using StemHttp.Core;

namespace StemWeb.Core.Controllers.Security
{
    [Authorize(Roles = "SystemAdmin, Developer, Owner, Users")]
    public class UsersController : BaseController<SysUser>
    {
        public UsersController(
            IHttpContextAccessor context,
            HttpRequestSrvice service,
            IConfiguration config) : base(context, service, config)
        {
            _resourceUrl = "Users";
        }
    }
}