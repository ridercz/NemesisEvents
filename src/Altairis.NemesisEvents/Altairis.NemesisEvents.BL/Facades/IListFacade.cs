using System.Collections.Generic;

namespace Altairis.NemesisEvents.BL.Facades {
    public interface IListFacade<TDto> {

        IList<TDto> GetList();

    }
}
