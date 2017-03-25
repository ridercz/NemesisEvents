using System;
using System.Threading.Tasks;
using Altairis.NemesisEvents.BL;
using Altairis.NemesisEvents.BL.Services;
using Altairis.NemesisEvents.DAL;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using Riganti.Utils.Infrastructure.Core;

namespace Altairis.NemesisEvents.Web.ViewModels {
    public class MasterPageViewModel : DotvvmViewModelBase {

        // Injected services

        [Bind(Direction.None)]
        public IOptions<AppConfiguration> Configuration { get; set; }

        [Bind(Direction.None)]
        public IHostingEnvironment Environment { get; set; }

        // Page title and route

        public virtual string Title { get; set; }

        public string FormattedTitle => string.IsNullOrWhiteSpace(this.Title) ? this.Configuration.Value.PageTitleDefault : string.Format(this.Configuration.Value.PageTitleFormat, this.Title);

        public string CurrentRoute => Context.Route.RouteName;

        // User identity
        public bool IsLoggedIn => Context.HttpContext.User.Identity.IsAuthenticated;

        public string LoggedUserName => Context.HttpContext.User.Identity.Name;

        public bool IsOrganizer => Context.HttpContext.User.IsInRole(Role.Organizers);

        public bool IsAdministrator => Context.HttpContext.User.IsInRole(Role.Administrators);

        public async Task Logout() {
            await Context.GetAuthentication().SignOutAsync(AppUserManager.AuthenticationScheme);
            Context.RedirectToRoute("Default");
        }

        // Error handling

        [Bind(Direction.ServerToClient)]
        public string ErrorMessage { get; set; }

        [Bind(Direction.ServerToClient)]
        public string SuccessMessage { get; set; }

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
                if (this.Environment.IsProduction()) {
                    this.ErrorMessage = "Došlo k vnitøní chybì.";
                }
                else {
                    this.ErrorMessage = ex.Message;
                }
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
                if (this.Environment.IsProduction()) {
                    this.ErrorMessage = "Došlo k vnitøní chybì.";
                }
                else {
                    this.ErrorMessage = ex.Message;
                }
                return false;
            }
        }

    }
}

