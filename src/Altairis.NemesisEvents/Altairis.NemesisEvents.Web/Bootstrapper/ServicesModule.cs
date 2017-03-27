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
using Altairis.NemesisEvents.BL;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Riganti.Utils.Infrastructure.Services.Azure.Storage;
using Riganti.Utils.Infrastructure.Services.Storage;

namespace Altairis.NemesisEvents.Web.Bootstrapper
{
    public class ServicesModule : Autofac.Module
    {
        
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(AppFacadeBase).GetTypeInfo().Assembly)
                .AssignableTo<FacadeBase>()
                .PropertiesAutowired()
                .InstancePerDependency();

            builder.Register(c => c.Resolve<IHostingEnvironment>().IsDevelopment() ? GetDevMailSender(c) : GetRealMailSender(c))
                .As<IMailSender>()
                .SingleInstance();
            
            builder.RegisterType<AppMailerService>()
                .SingleInstance();

            builder.Register(GetAzureStorageFolder)
                .SingleInstance();

            builder.Register(c => c.Resolve<IOptions<AppConfiguration>>().Value)
                .SingleInstance();
        }

        private static IStorageFolder GetAzureStorageFolder(IComponentContext c)
        {
            var options = c.Resolve<IOptions<AppConfiguration>>().Value;

            return AzureBlobStorageFolder.CreateContainerIfNotExists(
                CloudStorageAccount.Parse(options.AttachmentsStorageConnectionString),
                "attachments",
                BlobContainerPublicAccessType.Container
            );
        }

        private static IMailSender GetDevMailSender(IComponentContext c)
        {
            var options = c.Resolve<IOptions<AppConfiguration>>().Value;
            return new FileSystemMailSender(options.MailPickupDirectory);
        }

        private static IMailSender GetRealMailSender(IComponentContext c)
        {
            var options = c.Resolve<IOptions<AppConfiguration>>().Value;
            return new AmazonSESMailSender(options.AmazonAccessKeyId, options.AmazonSecretAccessKey);
        }
    }
}
