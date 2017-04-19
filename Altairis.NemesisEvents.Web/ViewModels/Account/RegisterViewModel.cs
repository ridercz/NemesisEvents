using System.Threading.Tasks;
using Altairis.NemesisEvents.BL.DTO;
using Altairis.NemesisEvents.BL.Facades;

namespace Altairis.NemesisEvents.Web.ViewModels.Account
{
	public class RegisterViewModel : MasterPageViewModel
	{
	    private readonly LoginFacade loginFacade;

	    public RegisterViewModel(LoginFacade loginFacade)
	    {
	        this.loginFacade = loginFacade;
	    }


	    public RegisterDTO Data { get; set; } = new RegisterDTO();

	    public async Task Register()
	    {
	        await ExecuteSafeAsync(async () =>
	        {
	            await loginFacade.RegisterUser(Data);
	            SuccessMessage = "Dìkujeme, na váš e-mail byl zaslán odkaz, kterým registraci dokonèíte.";
	            Data = new RegisterDTO();
	        });
	    }

	}
}

