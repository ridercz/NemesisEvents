using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Altairis.NemesisEvents.BL.Facades;
using Altairis.NemesisEvents.BL.Services;
using Altairis.NemesisEvents.BL.Services.Mailing;
using Autofac;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using Riganti.Utils.Infrastructure.Services.Amazon.SES;
using Riganti.Utils.Infrastructure.Services.Facades;
using Riganti.Utils.Infrastructure.Services.Mailing;

namespace Altairis.NemesisEvents.Web.Bootstrapper
{
    public class ServicesInstaller
    {
        public static void Install(ContainerBuilder builder, IHostingEnvironment env)
        {
            builder.RegisterAssemblyTypes(typeof(AppFacadeBase).GetTypeInfo().Assembly)
                .AssignableTo<FacadeBase>()
                .PropertiesAutowired()
                .InstancePerDependency();

            builder.Register(c => env.IsDevelopment() ? GetDevMailSender(c) : GetRealMailSender(c))
                .As<IMailSender>()
                .SingleInstance();
            
            builder.RegisterType<AppMailerService>()
                .SingleInstance();
        }

        private static IMailSender GetDevMailSender(IComponentContext c)
        {
            var options = c.Resolve<IOptions<AppConfig>>().Value;
            return new FileSystemMailSender(options.MailPickupDirectory);
        }

        private static IMailSender GetRealMailSender(IComponentContext c)
        {
            var options = c.Resolve<IOptions<AppConfig>>().Value;
            return new AmazonSESMailSender(options.AmazonAccessKeyId, options.AmazonSecretAccessKey);
        }
    }
}
