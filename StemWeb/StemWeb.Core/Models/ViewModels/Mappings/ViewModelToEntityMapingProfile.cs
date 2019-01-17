using AutoMapper;
using SharedStem.Core.Entities.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StemWeb.Core.Models.ViewModels.Mappings
{
    public class ViewModelToEntityMappingProfile : Profile
    {
        public ViewModelToEntityMappingProfile()
        {
            //CreateMap<ModuleFeaturesModel, AppModuleFeature>();//.ForMember(au => au.UserName, map => map.MapFrom(vm => vm.Email));
        }
    }
}
