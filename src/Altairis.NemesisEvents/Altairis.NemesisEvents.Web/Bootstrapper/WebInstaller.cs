using System.Reflection;
using Autofac;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.ViewModel;

namespace Altairis.NemesisEvents.Web.Bootstrapper
{
    public class WebInstaller
    {
        public static void Install(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(WebInstaller).GetTypeInfo().Assembly)
                .AssignableTo<DotvvmViewModelBase>()
                .InstancePerDependency();

            builder.RegisterAssemblyTypes(typeof(WebInstaller).GetTypeInfo().Assembly)
                .AssignableTo<IDotvvmPresenter>()
                .InstancePerDependency();
        }
    }
}