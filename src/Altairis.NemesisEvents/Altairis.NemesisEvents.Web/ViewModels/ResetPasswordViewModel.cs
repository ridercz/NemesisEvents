using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Altairis.NemesisEvents.BL.DTO;
using Altairis.NemesisEvents.BL.Facades;
using DotVVM.Framework.ViewModel;

namespace Altairis.NemesisEvents.Web.ViewModels
{
	public class ResetPasswordViewModel : MasterPageViewModel
	{
	    private readonly LoginFacade loginFacade;

	    public ResetPasswordViewModel(LoginFacade loginFacade)
	    {
	        this.loginFacade = loginFacade;
	    }


	    public ResetPasswordDTO Data { get; set; } = new ResetPasswordDTO();

	    public async Task ResetPassword()
	    {
	        await ExecuteSafeAsync(async () =>
	        {
	            var email = Context.Query["email"];
	            var token = Context.Query["token"];
	            await loginFacade.ResetPassword(Data, email, token);

	            SuccessMessage = "Heslo bylo nastaveno.";
	            Data = new ResetPasswordDTO();
	        });
	    }

    }
}

