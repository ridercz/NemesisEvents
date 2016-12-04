using System;
using System.Reflection;
using Altairis.NemesisEvents.BL.Services.Web;
using Autofac;
using Autofac.Core;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.ViewModel;
using Microsoft.Extensions.Options;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.Services;

namespace Altairis.NemesisEvents.Web.Bootstrapper {
    public class WebInstaller {
        public static void Install(ContainerBuilder builder) {
            builder.RegisterAssemblyTypes(typeof(WebInstaller).GetTypeInfo().Assembly)
                .AssignableTo<DotvvmViewModelBase>()
                .PropertiesAutowired(new DefaultPropertySelector(preserveSetValues: true))
                .InstancePerDependency();

            builder.RegisterAssemblyTypes(typeof(WebInstaller).GetTypeInfo().Assembly)
                .AssignableTo<IDotvvmPresenter>()
                .InstancePerDependency();

            builder.Register(c =>
                {
                    var options = c.Resolve<IOptions<AppConfig>>().Value;
                    return new WebRouteBuilder(options.BaseUrl, c.Resolve<DotvvmConfiguration>());
                })
                .As<IWebRouteBuilder>()
                .SingleInstance();

            builder.RegisterType<CurrentUserProvider>()
                .As<ICurrentUserProvider<int>>()
                .SingleInstance();
        }
    }
}