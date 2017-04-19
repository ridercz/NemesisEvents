using System.Threading.Tasks;
using Altairis.NemesisEvents.BL.Facades;
using DotVVM.Framework.Controls;
using DotVVM.Framework.ViewModel;

namespace Altairis.NemesisEvents.Web.ViewModels {
    public abstract class GridViewModelBase<TFacade, TListDto> : MasterPageViewModel where TFacade : IDataSourceFacade<TListDto> {

        // Data grid filling

        [Bind(Direction.None)]
        public TFacade Facade { get; set; }

        public GridViewDataSet<TListDto> Items { get; set; } = new GridViewDataSet<TListDto>() { PagingOptions = new PagingOptions { PageSize = 50 } };

        public override async Task PreRender() {
            this.Title = this.GetPageTitle();
            this.GetItems();
            await this.PreRenderInternal();
        }

        protected virtual void GetItems() {
            this.Facade.FillDataSet(this.Items);
        }

        // Extensibility points

        protected virtual Task PreRenderInternal() {
            return Task.CompletedTask;
        }

        protected abstract string GetPageTitle();

    }
}
