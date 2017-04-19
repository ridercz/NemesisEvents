using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Altairis.NemesisEvents.BL.DTO;
using Altairis.NemesisEvents.BL.Services.Web;
using Riganti.Utils.Infrastructure.Services.Mailing;

namespace Altairis.NemesisEvents.BL.Services.Mailing
{
    public class AppMailerService : MailerService
    {
        private readonly IWebRouteBuilder routeBuilder;

        public AppMailerService(IMailSender sender, IWebRouteBuilder routeBuilder) : base(sender)
        {
            this.routeBuilder = routeBuilder;

            SubjectFormatString = "[GeekCore] {0}";
        }

        public async Task SendPasswordResetEmailAsync(ForgottenPasswordDTO data, string token)
        {
            var url = routeBuilder.ResetPassword(data.Email, token).AbsoluteUri;
            await SendMailAsync(data.Email, "Reset hesla", url);
        }

        public async Task SendEmailAddressConfirmationEmailAsync(RegisterDTO data, string token)
        {
            var url = routeBuilder.VerifyEmail(data.Email, token).AbsoluteUri;
            await SendMailAsync(data.Email, "Ověření e-mailové adresy", url);
        }

        public async Task SendEmailChangeConfirmationEmailAsync(string newEmail, string token)
        {
            var url = routeBuilder.VerifyEmailChange(newEmail, token).AbsoluteUri;
            await SendMailAsync(newEmail, "Změna e-mailové adresy", url);
        }
    }
}
