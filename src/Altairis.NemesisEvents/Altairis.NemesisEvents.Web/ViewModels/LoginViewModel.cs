using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Altairis.NemesisEvents.BL.DTO;
using Altairis.NemesisEvents.BL.Facades;
using Altairis.NemesisEvents.BL.Services;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.ViewModel;

namespace Altairis.NemesisEvents.Web.ViewModels
{
	public class LoginViewModel : MasterPageViewModel
	{
	    private readonly LoginFacade loginFacade;

	    public LoginViewModel(LoginFacade loginFacade)
	    {
	        this.loginFacade = loginFacade;
	    }


	    public LoginDTO Data { get; set; } = new LoginDTO();


	    public async Task Login()
	    {
	        await ExecuteSafeAsync(async () =>
	        {
	            var identity = await loginFacade.Login(Data);
	            await Context.GetAuthentication().SignInAsync(AppUserManager.AuthenticationScheme, new ClaimsPrincipal(identity));
                Context.RedirectToRoute("Default");
	        });
	    }

	}
}

