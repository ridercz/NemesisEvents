﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Altairis.NemesisEvents.BL.Queries;
using Altairis.NemesisEvents.BL.Queries.FirstLevel;
using Altairis.NemesisEvents.BL.Repositories;
using Altairis.NemesisEvents.BL.Services;
using Altairis.NemesisEvents.DAL;
using Autofac;
using Autofac.Builder;
using Autofac.Core;
using Autofac.Core.Registration;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFrameworkCore;
using Riganti.Utils.Infrastructure.AutoMapper;
using Riganti.Utils.Infrastructure.Services.Facades;
using Altairis.NemesisEvents.BL;

namespace Altairis.NemesisEvents.Web.Bootstrapper
{
    public class DataAccessModule: Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>();

            builder.Register(c =>
                {
                    var options = c.Resolve<IOptions<AppConfiguration>>().Value;
                    return new NemesisEventsContext(options.SqlConnectionString);
                })
                .As<DbContext>()
                .As<NemesisEventsContext>()
                .InstancePerDependency();

            builder.Register(c => new AspNetCoreUnitOfWorkRegistry(c.Resolve<IHttpContextAccessor>(), new AsyncLocalUnitOfWorkRegistry()))
                .As<IUnitOfWorkRegistry>()
                .SingleInstance();

            builder.RegisterType<EntityFrameworkUnitOfWorkProvider>()
                .As<IUnitOfWorkProvider>()
                .SingleInstance();

            builder.RegisterGeneric(typeof(AppRepositoryBase<,>))
                .As(typeof(IRepository<,>))
                .SingleInstance();

            builder.RegisterType<UtcDateTimeProvider>()
                .As<IDateTimeProvider>()
                .SingleInstance();

            builder.RegisterAssemblyTypes(typeof(AppRepositoryBase<,>).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(AppRepositoryBase<,>))
                .SingleInstance();

            builder.RegisterAssemblyTypes(typeof(AppQueryBase<>).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(AppQueryBase<>))
                .InstancePerDependency();

            builder.RegisterAssemblyTypes(typeof(AppFirstLevelQueryBase<>).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(AppFirstLevelQueryBase<>))
                .InstancePerDependency();

            builder.RegisterGeneric(typeof(EntityDTOMapper<,>))
                .As(typeof(IEntityDTOMapper<,>))
                .SingleInstance();

            builder.RegisterType<AppUserStore>()
                .InstancePerDependency();

            builder.RegisterType<AppUserManager>()
                .InstancePerDependency();
        }
    }
}
