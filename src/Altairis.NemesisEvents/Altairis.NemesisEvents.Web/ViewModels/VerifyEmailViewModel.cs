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
	public class VerifyEmailViewModel : MasterPageViewModel
	{
	    private readonly LoginFacade loginFacade;

	    public VerifyEmailViewModel(LoginFacade loginFacade)
	    {
	        this.loginFacade = loginFacade;
	    }

	    public override async Task PreRender()
	    {
	        await ExecuteSafeAsync(async () =>
	        {
	            var email = Context.Query["email"];
	            var token = Context.Query["token"];

	            var identity = await loginFacade.VerifyEmail(email, token);
	            if (identity != null)
	            {
	                var principal = new ClaimsPrincipal(identity);
                    await Context.GetAuthentication().SignInAsync(AppUserManager.AuthenticationScheme, principal);
	                Context.GetAspNetCoreContext().User = principal;
                    SuccessMessage = "E-mailov� adresa byla �sp�n� ov��ena, nyn� jste p�ihl�eni.";
                }
	            else
	            {
	                SuccessMessage = "E-mailov� adresa ji� byla ov��ena.";
	            }
	        });
	        await base.PreRender();
	    }
	}
}

