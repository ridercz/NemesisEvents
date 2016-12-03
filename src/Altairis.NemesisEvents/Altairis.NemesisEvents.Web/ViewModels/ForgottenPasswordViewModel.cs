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
	public class ForgottenPasswordViewModel : MasterPageViewModel
	{
	    private readonly LoginFacade loginFacade;

	    public ForgottenPasswordViewModel(LoginFacade loginFacade)
	    {
	        this.loginFacade = loginFacade;
	    }

	    public ForgottenPasswordDTO Data { get; set; } = new ForgottenPasswordDTO();

	    public async Task SendResetPasswordEmail()
	    {
	        await ExecuteSafeAsync(async () =>
	        {
	            await loginFacade.SendResetPasswordEmail(Data);
	            SuccessMessage = "Na váš e-mail jsme zaslali návod k resetování hesla.";
                Data = new ForgottenPasswordDTO();
	        });
	    }

    }
}

