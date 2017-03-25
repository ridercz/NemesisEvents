using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using DotVVM.Framework.Controls.Infrastructure;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.ViewModel.Serialization;
using Microsoft.AspNetCore.Http;

namespace Altairis.NemesisEvents.Web.Bootstrapper
{
    public class AutofacViewModelLoader : DefaultViewModelLoader
    {
        private readonly IContainer container;
        private readonly IHttpContextAccessor httpContextAccessor;

        public AutofacViewModelLoader(IServiceProvider serviceProvider, IContainer container)
        {
            this.container = container;
            httpContextAccessor = serviceProvider.GetService(typeof(IHttpContextAccessor)) as IHttpContextAccessor;
        }

        protected override object CreateViewModelInstance(Type viewModelType, IDotvvmRequestContext context)
        {
            var scope = container.BeginLifetimeScope();
            httpContextAccessor.HttpContext.Items[typeof(AutofacViewModelLoader)] = scope;

            var instance = scope.Resolve(viewModelType);
            return instance;
        }

        public override void DisposeViewModel(object instance)
        {
            var scope = httpContextAccessor.HttpContext.Items[typeof(AutofacViewModelLoader)] as ILifetimeScope;
            scope?.Dispose();

            base.DisposeViewModel(instance);
        }
    }
}
