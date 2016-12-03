using System.Reflection;
using Altairis.NemesisEvents.BL.Mapping;
using Autofac;
using AutoMapper;

namespace Altairis.NemesisEvents.Web.Bootstrapper
{
    public class AutoMapperInstaller
    {
        public static void Install(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMapping).GetTypeInfo().Assembly)
                .AssignableTo<IMapping>()
                .As<IMapping>()
                .SingleInstance();
        }
    }
}