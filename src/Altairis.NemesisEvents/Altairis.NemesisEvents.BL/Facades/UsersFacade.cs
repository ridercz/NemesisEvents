using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Altairis.NemesisEvents.BL.DTO;
using Altairis.NemesisEvents.BL.Queries;

namespace Altairis.NemesisEvents.BL.Facades {
    public class UsersFacade : AppFacadeBase {

        public Func<UsersQuery> UsersQueryFactory { get; set; }

        public IList<UserDTO> List() {
            using (var uow = this.UnitOfWorkProvider.Create()) {
                var q = this.UsersQueryFactory();
                return q.Execute();
            }
        }

    }
}
