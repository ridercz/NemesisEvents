using DotVVM.Framework;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.ResourceManagement;
using DotVVM.Framework.Routing;

namespace Altairis.NemesisEvents.Web
{
    public class DotvvmStartup : IDotvvmStartup
    {
        // For more information about this class, visit https://dotvvm.com/docs/tutorials/basics-project-structure
        public void Configure(DotvvmConfiguration config, string applicationPath)
        {
            config.DefaultCulture = "cs-CZ";

            ConfigureRoutes(config, applicationPath);
            ConfigureControls(config, applicationPath);
            ConfigureResources(config, applicationPath);
        }

        private void ConfigureRoutes(DotvvmConfiguration config, string applicationPath)
        {
            config.RouteTable.Add("Default", "", "Views/Default.dothtml");

            config.RouteTable.Add("Login", "prihlaseni", "Views/Login.dothtml");
            config.RouteTable.Add("ForgottenPassword", "zapomenute-heslo", "Views/ForgottenPassword.dothtml");
            config.RouteTable.Add("ResetPassword", "reset-hesla", "Views/ResetPassword.dothtml");
            config.RouteTable.Add("Register", "registrace", "Views/Register.dothtml");
            config.RouteTable.Add("VerifyEmail", "overeni-emailu", "Views/VerifyEmail.dothtml");

            config.RouteTable.Add("EventDetail", "akce/{Id}/{Name}", "Views/EventDetail.dothtml");
            config.RouteTable.Add("Archive", "archiv", "Views/Archive.dothtml");

            // Uncomment the following line to auto-register all dothtml files in the Views folder
            // config.RouteTable.AutoDiscoverRoutes(new DefaultRouteStrategy(config));    
        }

        private void ConfigureControls(DotvvmConfiguration config, string applicationPath)
        {
            // register code-only controls and markup controls
            config.Markup.AddMarkupControl("cc", "Menu", "Controls/Menu.dotcontrol");
            config.Markup.AddMarkupControl("cc", "Alerts", "Controls/Alerts.dotcontrol");
        }

        private void ConfigureResources(DotvvmConfiguration config, string applicationPath)
        {
            // register custom resources and adjust paths to the built-in resources
            config.Resources.Register("bootstrap", new ScriptResource()
            {
                Url = "~/js/bootstrap.min.js",
                Dependencies = new [] { "jquery" }
            });
        }
    }
}
