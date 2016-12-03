using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Altairis.NemesisEvents.BL.DTO {
    public class OrganizedEventDTO {

        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime DateBegin { get; set; }

        public DateTime DateEnd { get; set; }

        public string VenueCity { get; set; }

        public string VenueName { get; set; }

        public string VenueDisplayName => string.Join(" - ", this.VenueCity, this.VenueName);

        public bool UseRegistration { get; set; }

        public bool AllowRegistration { get; set; }

        public string OrganizerName { get; set; }

        public bool HasAdmissionFee { get; set; }

        public DateTime DateCreated { get; set; }

        public bool InvitationSent { get; set; }

        public int AttendeesCount { get; set; }

        public bool HasVideo { get; set; }

        public bool HasSlides { get; set; }

        public bool HasDemo { get; set; }

        public bool HasPhotos { get; set; }

        public bool HasOtherAttachments { get; set; }

    }
}
