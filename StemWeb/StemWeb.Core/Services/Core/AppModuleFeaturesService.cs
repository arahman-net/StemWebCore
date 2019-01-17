using Microsoft.AspNetCore.Http;
using SharedStem.Core.Entities.App;
using StemHttp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StemWeb.Core.Services.Core
{
    public class AppModuleFeaturesService : BaseService<AppModuleFeature>
    {
        public AppModuleFeaturesService(IHttpContextAccessor context,
            HttpRequestSrvice requestService) : base(context,requestService) { }

        //public List<AppFeature> GetFeaturesList()
        //{
        //    var query = $"AppFeatures";

        //    var appFeatures = requestService.GetAsync<AppFeature[]>(
        //                    query, BearerToken);

        //    List<Speciality> list = new List<Speciality>();
        //    list.AddRange(Specialities);
        //    return list;
        //}
    }
}
