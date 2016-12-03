using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace Altairis.NemesisEvents.BL.Services.Web
{
    public interface IWebRouteBuilder
    {

        WebRoute Default();

        WebRoute Login();

        WebRoute ForgottenPassword();

        WebRoute ResetPassword(string email, string token);

    }
}
