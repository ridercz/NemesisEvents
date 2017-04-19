using System.Threading.Tasks;
using Altairis.NemesisEvents.BL.Facades;
using DotVVM.Framework.ViewModel;

namespace Altairis.NemesisEvents.Web.ViewModels {
    public abstract class EditViewModelBase<TFacade, TDto, TKey> : MasterPageViewModel where TDto : class where TFacade : ICrudFacade<TDto, TKey> {

        [Bind(Direction.None)]
        public TFacade Facade { get; set; }

        public TDto Item { get; set; }

        public async override Task PreRender() {
            this.Item = this.Item ?? this.Facade.GetDetail(this.ItemId);
            this.Title = this.GetPageTitle();
            await this.PreRenderInternal();
        }

        public virtual Task Submit() {
            this.ExecuteSafe(() => {
                this.Facade.Save(this.Item);
                this.RedirectToContinue();
            });

            return Task.CompletedTask;
        }

        protected void RedirectToContinue() {
            this.Context.RedirectToRoute(this.ContinuePageRouteName, this.ContinuePageRouteParams);
        }

        protected abstract TKey ItemId { get; }

        protected abstract string ContinuePageRouteName { get; }

        protected virtual object ContinuePageRouteParams { get; }

        protected virtual Task PreRenderInternal() {
            return Task.CompletedTask;
        }

        protected abstract string GetPageTitle();

    }
}
