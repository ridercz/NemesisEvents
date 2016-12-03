using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Altairis.NemesisEvents.BL.Facades;
using Altairis.NemesisEvents.BL.Services;
using Altairis.NemesisEvents.BL.Services.Mailing;
using Autofac;
using Riganti.Utils.Infrastructure.Services.Facades;
using Riganti.Utils.Infrastructure.Services.Mailing;

namespace Altairis.NemesisEvents.Web.Bootstrapper
{
    public class ServicesInstaller
    {
        public static void Install(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(AppFacadeBase).GetTypeInfo().Assembly)
                .AssignableTo<FacadeBase>()
                .PropertiesAutowired()
                .InstancePerDependency();

            builder.RegisterType<SendGridMailSender>()
                .As<IMailSender>()
                .SingleInstance();

            builder.RegisterType<AppMailerService>()
                .SingleInstance();
        }
    }
}
