using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SharedStem.Core.Entities.App;
using StemHttp.Core;

namespace StemWeb.Core.Controllers.Security
{
    [Authorize(Roles = "SystemAdmin, Developer, Owner, AppFeatures")]
    public class AppFeaturesController : BaseController<AppFeature>
    {
        public AppFeaturesController(
            IHttpContextAccessor context,
            HttpRequestSrvice service,
            IConfiguration config) : base(context, service, config) { }
    }

    //also add feture type like Main feature and sub feature
    //sub features can be added that are not menu features but will be added for
    // sub control access

}