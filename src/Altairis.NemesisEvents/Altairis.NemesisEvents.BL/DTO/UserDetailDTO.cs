using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Riganti.Utils.Infrastructure.Core;

namespace Altairis.NemesisEvents.BL.DTO {
    public class UserDetailDTO : IEntity<int> {

        public int Id { get; set; }

        public string Email { get; set; }

        public string FullName { get; set; }

        public string CompanyName { get; set; }

        public bool Enabled { get; set; }

        public bool IsAdministrator { get; set; }

        public bool IsOrganizer { get; set; }

    }
}
