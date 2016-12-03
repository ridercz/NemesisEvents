using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Altairis.NemesisEvents.Web.Bootstrapper;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.Security;
using AutoMapper;
using Altairis.NemesisEvents.BL.Mapping;

namespace Altairis.NemesisEvents.Web
{
    public class Startup
    {
        public IContainer ApplicationContainer { get; private set; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddDataProtection();
            services.AddAuthorization();
            services.AddWebEncoders();

            services.AddDotVVM()
                .ConfigureTempStorages("temp");

            // configure container
            var builder = new ContainerBuilder();
            DataAccessInstaller.Install(builder);
            AutoMapperInstaller.Install(builder);
            ServicesInstaller.Install(builder);
            WebInstaller.Install(builder);
            builder.Populate(services);
            ApplicationContainer = builder.Build();

            return new AutofacServiceProvider(ApplicationContainer);
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApplicationLifetime appLifetime)
        {
            // use DotVVM
            var dotvvmConfiguration = app.UseDotVVM<DotvvmStartup>(env.ContentRootPath);

            // use static files
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(env.WebRootPath)
            });

            // dispose container
            appLifetime.ApplicationStopped.Register(() => this.ApplicationContainer.Dispose());

            // init AutoMapper
            Mapper.Initialize(cfg =>
            {
                foreach (var mapping in this.ApplicationContainer.Resolve<IEnumerable<IMapping>>())
                {
                    mapping.Map(cfg);
                }
            });
        }
    }
}
