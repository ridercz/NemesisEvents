using DotVVM.Framework;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.ResourceManagement;
using DotVVM.Framework.Routing;

namespace Altairis.NemesisEvents.Web {
    public class DotvvmStartup : IDotvvmStartup {
        // For more information about this class, visit https://dotvvm.com/docs/tutorials/basics-project-structure
        public void Configure(DotvvmConfiguration config, string applicationPath) {
            config.DefaultCulture = "cs-CZ";

            ConfigureRoutes(config, applicationPath);
            ConfigureControls(config, applicationPath);
            ConfigureResources(config, applicationPath);
        }

        private void ConfigureRoutes(DotvvmConfiguration config, string applicationPath) {
            config.RouteTable.Add("Default", "", "Views/Default.dothtml");

            config.RouteTable.Add("Login", "login", "Views/Account/Login.dothtml");
            config.RouteTable.Add("ForgottenPassword", "reset", "Views/Account/ForgottenPassword.dothtml");
            config.RouteTable.Add("ResetPassword", "reset/verify", "Views/Account/ResetPassword.dothtml");
            config.RouteTable.Add("Register", "reg", "Views/Account/Register.dothtml");
            config.RouteTable.Add("VerifyEmail", "reg/verify", "Views/Account/VerifyEmail.dothtml");

            config.RouteTable.Add("EventDetail", "events/{EventId}", "Views/EventDetail.dothtml");
            config.RouteTable.Add("Archive", "events", "Views/Archive.dothtml");

            config.RouteTable.Add("MyDefault", "my", "Views/My/Default.dothtml");
            config.RouteTable.Add("MyProfile", "my/profile", "Views/My/Profile.dothtml");

            config.RouteTable.Add("OrganizerEvents", "organizer/events", "Views/Organizer/Events.dothtml");
            config.RouteTable.Add("OrganizerEventsNew", "organizer/events/new", "Views/Organizer/EventsNew.dothtml");
            config.RouteTable.Add("OrganizerEventsEdit", "organizer/events/{EventId}", "Views/Organizer/EventsEdit.dothtml");
            config.RouteTable.Add("OrganizerVenues", "organizer/venues", "Views/Organizer/Venues.dothtml");
            config.RouteTable.Add("OrganizerVenuesEdit", "organizer/venues/{VenueId?}", "Views/Organizer/VenuesEdit.dothtml");

            config.RouteTable.Add("AdminEvents", "admin/events", "Views/Admin/Events.dothtml");
            config.RouteTable.Add("AdminEventsNew", "admin/events/new", "Views/Admin/EventsNew.dothtml");
            config.RouteTable.Add("AdminEventsEdit", "admin/events/{EventId}", "Views/Admin/EventsEdit.dothtml");
            config.RouteTable.Add("AdminUsers", "admin/users", "Views/Admin/Users.dothtml");
            config.RouteTable.Add("AdminUsersEdit", "admin/users/{UserId?}", "Views/Admin/UsersEdit.dothtml");
            config.RouteTable.Add("AdminVenues", "admin/venues", "Views/Admin/Venues.dothtml");
            config.RouteTable.Add("AdminVenuesEdit", "admin/venues/{VenueId?}", "Views/Admin/VenuesEdit.dothtml");
            config.RouteTable.Add("AdminTags", "admin/tags", "Views/Admin/Tags.dothtml");
            config.RouteTable.Add("AdminTagsEdit", "admin/tags/{TagId?}", "Views/Admin/TagsEdit.dothtml");

            // Uncomment the following line to auto-register all dothtml files in the Views folder
            // config.RouteTable.AutoDiscoverRoutes(new DefaultRouteStrategy(config));
        }

        private void ConfigureControls(DotvvmConfiguration config, string applicationPath) {
            // register code-only controls and markup controls
            config.Markup.AddMarkupControl("cc", "Menu", "Controls/Menu.dotcontrol");
            config.Markup.AddMarkupControl("cc", "OrganizerMenu", "Controls/OrganizerMenu.dotcontrol");
            config.Markup.AddMarkupControl("cc", "AdminMenu", "Controls/AdminMenu.dotcontrol");
            config.Markup.AddMarkupControl("cc", "Alerts", "Controls/Alerts.dotcontrol");
        }

        private void ConfigureResources(DotvvmConfiguration config, string applicationPath) {
            // register custom resources and adjust paths to the built-in resources
            config.Resources.Register("bootstrap", new ScriptResource() {
                Location = new UrlResourceLocation("~/js/bootstrap.min.js"),
                Dependencies = new[] { "jquery" }
            });
        }
    }
}
