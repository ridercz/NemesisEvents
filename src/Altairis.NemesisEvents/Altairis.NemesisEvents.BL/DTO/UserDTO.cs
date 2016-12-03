using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Altairis.NemesisEvents.DAL;

namespace Altairis.NemesisEvents.BL.DTO {
    public class UserDTO {
        public int Id { get; set; }

        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public string FullName { get; set; }

        public string CompanyName { get; set; }

        public EmailFrequency EmailFrequency { get; set; }

        public int OwnedEventsCount { get; set; }

        public int AttendancesCount { get; set; }

    }
}
