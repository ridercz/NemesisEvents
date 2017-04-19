using DotVVM.Framework.Controls;

namespace Altairis.NemesisEvents.BL.Facades {
    public interface IDataSourceFacade<TListDto> {

        void FillDataSet(GridViewDataSet<TListDto> dataSet);

    }
}
