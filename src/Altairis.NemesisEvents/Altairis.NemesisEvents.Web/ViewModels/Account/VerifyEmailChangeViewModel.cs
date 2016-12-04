using System.Threading.Tasks;
using Altairis.NemesisEvents.BL.Facades;

namespace Altairis.NemesisEvents.Web.ViewModels.Account
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

