using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Altairis.NemesisEvents.BL.DTO;
using Altairis.NemesisEvents.BL.Queries;
using Altairis.NemesisEvents.DAL;

namespace Altairis.NemesisEvents.BL.Facades
{
    public class BaseListsFacade : AppFacadeBase
    {

        public Func<OrganizerUsersBasicQuery> OrganizerUsersBasicQuery { get; set; }

        public Func<VenuesBasicQuery> VenuesBasicQuery { get; set; }

        public Func<TagsQuery> TagsQuery { get; set; }




        public List<UserBasicDTO> GetOrganizerOrAdminUsers()
        {
            using (UnitOfWorkProvider.Create())
            {
                var q = OrganizerUsersBasicQuery();
                return q.Execute().ToList();
            }
        }

        public List<VenueBasicDTO> GetVenues()
        {
            using (UnitOfWorkProvider.Create())
            {
                var q = VenuesBasicQuery();
                return q.Execute().ToList();
            }
        }

        public List<TagDTO> GetTags()
        {
            using (UnitOfWorkProvider.Create())
            {
                var q = TagsQuery();
                return q.Execute().ToList();
            }
        }

        public List<AttachmentTypeDTO> GetAttachmentTypes()
        {
            return new List<AttachmentTypeDTO>()
            {
                new AttachmentTypeDTO() {Id = AttachmentType.Slides, Name = "Prezentace"},
                new AttachmentTypeDTO() {Id = AttachmentType.Demo, Name = "Demo"},
                new AttachmentTypeDTO() {Id = AttachmentType.Photo, Name = "Fotografie"},
                new AttachmentTypeDTO() {Id = AttachmentType.Other, Name = "Ostatní"}
            };
        }
    }
}
