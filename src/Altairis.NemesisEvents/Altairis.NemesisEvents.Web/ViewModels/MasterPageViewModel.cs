using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Altairis.NemesisEvents.BL.Services;
using Altairis.NemesisEvents.DAL;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.ViewModel;
using Microsoft.Extensions.Options;
using Riganti.Utils.Infrastructure.Core;

namespace Altairis.NemesisEvents.Web.ViewModels {
    public class MasterPageViewModel : DotvvmViewModelBase {

        [Bind(Direction.None)]
        public IOptions<AppConfig> Configuration { get; set; }

        public virtual string Title { get; set; }

        public string FormattedTitle => string.IsNullOrWhiteSpace(this.Title) ? this.Configuration.Value.PageTitleDefault : string.Format(this.Configuration.Value.PageTitleFormat, this.Title);

        [Bind(Direction.ServerToClient)]
        public string ErrorMessage { get; set; }

        [Bind(Direction.ServerToClient)]
        public string SuccessMessage { get; set; }

        public bool IsLoggedIn => Context.HttpContext.User.Identity.IsAuthenticated;

        public string LoggedUserName => Context.HttpContext.User.Identity.Name;

        public bool IsOrganizer => Context.HttpContext.User.IsInRole(Role.Organizers);

        public bool IsAdministrator => Context.HttpContext.User.IsInRole(Role.Administrators);

        public string CurrentRoute => Context.Route.RouteName;

        public async Task Logout() {
            await Context.GetAuthentication().SignOutAsync(AppUserManager.AuthenticationScheme);
            Context.RedirectToRoute("Default");
        }

        protected bool ExecuteSafe(Action action) {
            try {
                action();
                return true;
            }
            catch (DotvvmInterruptRequestExecutionException) {
                throw;
            }
            catch (UIException ex) {
                ErrorMessage = ex.Message;
                return false;
            }
            catch (Exception ex) {
                ErrorMessage = "Do�lo k nezn�m� chyb�.";
                return false;
            }
        }

        protected async Task<bool> ExecuteSafeAsync(Func<Task> action) {
            try {
                await action();
                return true;
            }
            catch (DotvvmInterruptRequestExecutionException) {
                throw;
            }
            catch (UIException ex) {
                ErrorMessage = ex.Message;
                return false;
            }
            catch (Exception ex) {
                ErrorMessage = "Do�lo k nezn�m� chyb�.";
                return false;
            }
        }

    }
}

