using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SharedStem.Core.Entities.Security;
using StemHttp.Core;

namespace StemWeb.Core.Controllers.Security
{
    [Authorize(Roles = "SystemAdmin, Developer, Owner, SecurityGroups")]
    public class SecurityGroupsController : BaseController<SecurityGroup>
    {
        public SecurityGroupsController(
            IHttpContextAccessor context,
            HttpRequestSrvice service,
            IConfiguration config) : base(context, service, config) { }
    }
}