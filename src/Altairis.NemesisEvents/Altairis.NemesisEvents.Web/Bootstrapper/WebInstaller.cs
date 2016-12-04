using System;
using System.Reflection;
using Altairis.NemesisEvents.BL.Services.Web;
using Autofac;
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
                //.PropertiesAutowired()
                .InstancePerDependency()
                .OnActivated(args => {
                    // Autofac does not support property injection in base classes
                    var vm = args.Instance as ViewModels.MasterPageViewModel;
                    if (vm != null) {
                        vm.Configuration = args.Context.Resolve<IOptions<AppConfig>>();
                    }
                });

            builder.RegisterAssemblyTypes(typeof(WebInstaller).GetTypeInfo().Assembly)
                .AssignableTo<IDotvvmPresenter>()
                .InstancePerDependency();

            builder.Register(c => new WebRouteBuilder("http://localhost:5194/", c.Resolve<DotvvmConfiguration>()))
                .As<IWebRouteBuilder>()
                .SingleInstance();

            builder.RegisterType<CurrentUserProvider>()
                .As<ICurrentUserProvider<int>>()
                .SingleInstance();
        }
    }
}