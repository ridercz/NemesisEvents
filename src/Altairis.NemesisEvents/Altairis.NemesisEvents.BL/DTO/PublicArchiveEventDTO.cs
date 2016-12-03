using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Altairis.NemesisEvents.BL.DTO {
    public class PublicArchiveEventDTO {

        public int Id { get; set; }

        public string Name { get; set; }

        public string NameUrl => TextUtils.ToUrlName(Name);

        public DateTime DateBegin { get; set; }

        public DateTime DateEnd { get; set; }

        public string OrganizerName { get; set; }

        public string VenueCity { get; set; }

        public string VenueName { get; set; }

        public string VenueDisplayName => string.Join(" - ", this.VenueCity, this.VenueName);

        public IEnumerable<string> Tags { get; set; }

        public bool HasVideo { get; set; }

        public bool HasSlides { get; set; }

        public bool HasDemo { get; set; }

        public bool HasPhotos { get; set; }

        public bool HasOtherAttachments { get; set; }

        public string TagsDisplayText => string.Join(", ", this.Tags);

    }
}
