using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Altairis.NemesisEvents.BL.DTO;
using Altairis.NemesisEvents.BL.Queries;

namespace Altairis.NemesisEvents.BL.Facades
{
    public class TagsFacade: AppFacadeBase
    {

        public Func<TagsQuery> QueryFactory { get; set; }

        public IList<TagDTO> GetList() {
            using (var uow = this.UnitOfWorkProvider.Create()) {
                return this.QueryFactory().Execute();
            }
        }

    }
}
