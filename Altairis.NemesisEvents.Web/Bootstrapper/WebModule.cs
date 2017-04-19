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
using Altairis.NemesisEvents.BL;

namespace Altairis.NemesisEvents.Web.Bootstrapper
{
    public class WebModule: Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(WebModule).GetTypeInfo().Assembly)
                .AssignableTo<DotvvmViewModelBase>()
                .PropertiesAutowired(new DefaultPropertySelector(preserveSetValues: true))
                .InstancePerDependency();

            builder.RegisterAssemblyTypes(typeof(WebModule).GetTypeInfo().Assembly)
                .AssignableTo<IDotvvmPresenter>()
                .InstancePerDependency();

            builder.Register(c =>
                {
                    var options = c.Resolve<IOptions<AppConfiguration>>().Value;
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