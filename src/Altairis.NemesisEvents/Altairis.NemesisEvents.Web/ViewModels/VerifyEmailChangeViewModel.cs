using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Altairis.NemesisEvents.BL.Facades;
using Altairis.NemesisEvents.BL.Services;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.ViewModel;

namespace Altairis.NemesisEvents.Web.ViewModels
{
	public class VerifyEmailChangeViewModel : MasterPageViewModel
	{
        private readonly LoginFacade loginFacade;

        public VerifyEmailChangeViewModel(LoginFacade loginFacade)
        {
            this.loginFacade = loginFacade;
        }

        public override async Task PreRender()
        {
            await ExecuteSafeAsync(async () =>
            {
                var email = Context.Query["email"];
                var token = Context.Query["token"];

                await loginFacade.ChangeEmail(email, token);
                SuccessMessage = "E-mailová adresa byla úspìšnì zmìnìna.";
            });
            await base.PreRender();
        }

    }
}

