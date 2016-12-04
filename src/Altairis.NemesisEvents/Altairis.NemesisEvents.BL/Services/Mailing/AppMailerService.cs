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

        public void SendPasswordResetEmail(ForgottenPasswordDTO data, string token)
        {
            var url = routeBuilder.ResetPassword(data.Email, token).AbsoluteUri;
            SendMail(data.Email, "Reset hesla", url);
        }

        public void SendEmailAddressConfirmationEmail(RegisterDTO data, string token)
        {
            var url = routeBuilder.VerifyEmail(data.Email, token).AbsoluteUri;
            SendMail(data.Email, "Ověření e-mailové adresy", url);
        }
    }
}
