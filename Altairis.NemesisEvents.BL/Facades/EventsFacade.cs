using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Threading.Tasks;
using Altairis.NemesisEvents.BL.DTO;
using Altairis.NemesisEvents.BL.Queries;
using Altairis.NemesisEvents.DAL;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.Services;
using Riganti.Utils.Infrastructure.Services.Storage;

namespace Altairis.NemesisEvents.BL.Facades {
    public class EventsFacade : AppCrudFacadeBase<Event, int, EventDTO, EventDetailDTO> {

        public IStorageFolder AttachmentsStorageFolder { get; set; }

        public AppConfiguration AppConfiguration { get; set; }


        public EventsFacade(Func<EventsQuery> queryFactory, IRepository<Event, int> repository, Riganti.Utils.Infrastructure.Services.Facades.IEntityDTOMapper<Event, EventDetailDTO> mapper) : base(queryFactory, repository, mapper) {
        }

        public Func<OrganizedEventsQuery> OrganizedEventsQueryFactory { get; set; }

        public ICurrentUserProvider<int> CurrentUser { get; set; }

        public override void Save(EventDetailDTO detail) {
            // Force current user ID to non-admins
            if (!this.CurrentUser.IsInRole(Role.Administrators)) detail.OwnerId = this.CurrentUser.Id;

            base.Save(detail);
        }

        public override IEnumerable<EventDTO> GetList(Action<IQuery<EventDTO>> queryConfiguration = null) {
            if (!this.CurrentUser.IsInRole(Role.Administrators)) throw new SecurityException("Only administrators can access events owned by other users.");

            return base.GetList(queryConfiguration);
        }

        public override EventDetailDTO GetDetail(int id) {
            var d = base.GetDetail(id);
            if (d.OwnerId != this.CurrentUser.Id && !this.CurrentUser.IsInRole(Role.Administrators)) throw new SecurityException("Only administrators can access events owned by other users.");
            return d;
        }

        public IList<OrganizedEventDTO> ListEventsOrganizedBy(int userId = 0) {
            // Default to current user
            if (userId == 0) userId = this.CurrentUser.Id;

            using (var uow = this.UnitOfWorkProvider.Create()) {
                var q = this.OrganizedEventsQueryFactory();
                q.UserId = userId;
                return q.Execute();
            }
        }

        public async Task<AttachmentDTO> UploadAttachmentAsync(int eventId, string fileName, Stream stream)
        {
            // generate unique filename
            var url = $"{eventId}/{Path.GetFileName(fileName)}";
            var uniqueIndex = 0;
            while (await AttachmentsStorageFolder.FileExists(url))
            {
                uniqueIndex++;
                url = $"{eventId}/{Path.GetFileNameWithoutExtension(fileName)}_{uniqueIndex}{Path.GetExtension(fileName)}";
            }

            // upload file
            await AttachmentsStorageFolder.SaveFile(url, stream);
            
            return new AttachmentDTO()
            {
                EventId = eventId,
                Name = Path.GetFileNameWithoutExtension(url),
                StorageUrl = AppConfiguration.AttachmentsStoragePublicUrl + url
            };
        }
    }
}
