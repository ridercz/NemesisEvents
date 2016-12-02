using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Altairis.NemesisEvents.BL.Facades;
using DotVVM.Framework.ViewModel;

namespace Altairis.NemesisEvents.Web.ViewModels
{
    public class DefaultViewModel : DotvvmViewModelBase
    {
        
        public string Title { get; set; }


        public DefaultViewModel(LoginFacade loginFacade)
        {
            Title = "Hello from DotVVM!";
        }

    }
}
