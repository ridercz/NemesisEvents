using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Altairis.NemesisEvents.BL.Services.Web;
using DotVVM.Framework.Configuration;

namespace Altairis.NemesisEvents.Web
{
    public class WebRouteBuilder : IWebRouteBuilder
    {
        private readonly string domain;
        private readonly DotvvmConfiguration configuration;

        public WebRouteBuilder(string domain, DotvvmConfiguration configuration)
        {
            this.domain = domain;
            this.configuration = configuration;
        }

        public WebRoute Default()
        {
            return new WebRoute(domain, configuration.RouteTable["Default"].BuildUrl(new { }));
        }

        public WebRoute Login()
        {
            return new WebRoute(domain, configuration.RouteTable["Login"].BuildUrl(new { }));
        }

        public WebRoute ForgottenPassword()
        {
            return new WebRoute(domain, configuration.RouteTable["ForgottenPassword"].BuildUrl(new { }));
        }

        public WebRoute ResetPassword(string email, string token)
        {
            return new WebRoute(domain, configuration.RouteTable["ResetPassword"].BuildUrl(new {})
                + "?email=" + WebUtility.UrlEncode(email) + "&token=" + WebUtility.UrlEncode(token));
        }
    }
}
