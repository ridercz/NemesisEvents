using System;
using System.Collections.Generic;

namespace Altairis.NemesisEvents.BL.DTO {
    public class PublicUpcomingEventDTO {

        public int Id { get; set; }

        public string Name { get; set; }

        public string NameUrl => TextUtils.ToUrlName(Name);

        public DateTime DateBegin { get; set; }

        public DateTime DateEnd { get; set; }

        public string VenueCity { get; set; }

        public string VenueName { get; set; }

        public string VenueDisplayName => this.VenueCity == null ? this.VenueName : string.Join(" - ", this.VenueCity, this.VenueName);

        public string OrganizerName { get; set; }

        public bool HasAdmissionFee { get; set; }

        public IEnumerable<string> Tags { get; set; }

        public string TagsDisplayText => string.Join(", ", this.Tags);

    }
}