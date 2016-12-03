using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.ViewModel;
using Riganti.Utils.Infrastructure.Core;

namespace Altairis.NemesisEvents.Web.ViewModels
{
	public class MasterPageViewModel : DotvvmViewModelBase
	{
		public string Title { get; set; } = "Nemesis Events";


        [Bind(Direction.ServerToClient)]
	    public string ErrorMessage { get; set; }

        [Bind(Direction.ServerToClient)]
        public string SuccessMessage { get; set; }

	    public bool IsLoggedIn => Context.HttpContext.User.Identity.IsAuthenticated;

	    public string LoggedUserName => Context.HttpContext.User.Identity.Name;

        public string CurrentRoute => Context.Route.RouteName;


        protected bool ExecuteSafe(Action action)
	    {
	        try
	        {
	            action();
	            return true;
	        }
	        catch (DotvvmInterruptRequestExecutionException)
	        {
	            throw;
	        }
	        catch (UIException ex)
	        {
	            ErrorMessage = ex.Message;
	            return false;
	        }
	        catch (Exception ex)
	        {
	            ErrorMessage = "Došlo k neznámé chybì.";
	            return false;
	        }
	    }

        protected async Task<bool> ExecuteSafeAsync(Func<Task> action)
        {
            try
            {
                await action();
                return true;
            }
            catch (DotvvmInterruptRequestExecutionException)
            {
                throw;
            }
            catch (UIException ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
            catch (Exception ex)
            {
                ErrorMessage = "Došlo k neznámé chybì.";
                return false;
            }
        }
    }
}

