using System;
using System.Collections.Generic;
using System.Reflection;
using Altairis.NemesisEvents.Web.Bootstrapper;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Altairis.NemesisEvents.BL.Mapping;
using Altairis.NemesisEvents.BL.Services;
using Altairis.NemesisEvents.DAL;
using DotVVM.Framework.ViewModel.Serialization;
using Microsoft.Extensions.Configuration;
using Altairis.NemesisEvents.BL;

namespace Altairis.NemesisEvents.Web {
    public class Startup {
        private readonly IHostingEnvironment env;

        public IContainer ApplicationContainer { get; private set; }
        public IConfigurationRoot Configuration { get; private set; }

        public Startup(IHostingEnvironment env) {
            this.env = env;
            // Load configuration
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(env.ContentRootPath);
            builder.AddJsonFile("appconfig.json");                                          // 1: General JSON file
            builder.AddJsonFile($"appconfig.{env.EnvironmentName}.json", optional: true);   // 2: Environment specific JSON file
            builder.AddUserSecrets(typeof(Startup).GetTypeInfo().Assembly);                                                       // 3: User secrets
            builder.AddEnvironmentVariables();                                              // 4: Environment variables
            this.Configuration = builder.Build();
        }

        public IServiceProvider ConfigureServices(IServiceCollection services) {
            services.AddDataProtection();
            services.AddAuthorization();
            services.AddWebEncoders();

            services.AddOptions();
            services.Configure<AppConfiguration>(this.Configuration);

            services.AddDotVVM(options =>
            {
                options.AddDefaultTempStorages("temp");
            });
                
            services.AddSingleton<IViewModelLoader>(p => new AutofacViewModelLoader(p, this.ApplicationContainer));

            services.AddIdentity<User, Role>(options => {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 12;
                options.User.RequireUniqueEmail = true;
            })
            .AddDefaultTokenProviders();

            // configure container
            var builder = new ContainerBuilder();
            DataAccessInstaller.Install(builder);
            AutoMapperInstaller.Install(builder);
            ServicesInstaller.Install(builder, env);
            WebInstaller.Install(builder);
            builder.Populate(services);
            ApplicationContainer = builder.Build();

            return new AutofacServiceProvider(ApplicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApplicationLifetime appLifetime) {
            // cookie authentication
            app.UseCookieAuthentication(new CookieAuthenticationOptions() {
                AuthenticationScheme = AppUserManager.AuthenticationScheme,
                LoginPath = new PathString("/prihlaseni"),
                AccessDeniedPath = new PathString("/"),
                AutomaticAuthenticate = true,
                AutomaticChallenge = true
            });

            // use DotVVM
            var dotvvmConfiguration = app.UseDotVVM<DotvvmStartup>(env.ContentRootPath);

            // use static files
            app.UseStaticFiles(new StaticFileOptions {
                FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(env.WebRootPath)
            });

            // dispose container
            appLifetime.ApplicationStopped.Register(() => this.ApplicationContainer.Dispose());

            // init AutoMapper
            Mapper.Initialize(cfg => {
                foreach (var mapping in this.ApplicationContainer.Resolve<IEnumerable<IMapping>>()) {
                    mapping.Map(cfg);
                }
            });
        }
    }
}
