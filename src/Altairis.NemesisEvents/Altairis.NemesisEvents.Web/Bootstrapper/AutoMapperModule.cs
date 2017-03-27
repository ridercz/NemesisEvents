using System.Reflection;
using Altairis.NemesisEvents.BL.Mapping;
using Autofac;

namespace Altairis.NemesisEvents.Web.Bootstrapper
{
    public class AutoMapperModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMapping).GetTypeInfo().Assembly)
                .AssignableTo<IMapping>()
                .As<IMapping>()
                .SingleInstance();
        }
        
    }
}