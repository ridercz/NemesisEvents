using DotVVM.Framework.ViewModel;
using Riganti.Utils.Infrastructure.Core;

namespace Altairis.NemesisEvents.BL.DTO {
    public class UserDetailDTO : IEntity<int> {

        public int Id { get; set; }

        [Bind(Direction.ServerToClient)]
        public string Email { get; set; }

        [Bind(Direction.ServerToClient)]
        public string FullName { get; set; }

        [Bind(Direction.ServerToClient)]
        public string CompanyName { get; set; }

        public bool Enabled { get; set; }

        public bool IsAdministrator { get; set; }

        public bool IsOrganizer { get; set; }

    }
}
