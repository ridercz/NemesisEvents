using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Altairis.NemesisEvents.BL.DTO;
using Altairis.NemesisEvents.BL.Facades;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Storage;
using DotVVM.Framework.ViewModel;

namespace Altairis.NemesisEvents.Web.ViewModels.Admin {
    public class EventsEditViewModel : EditViewModelBase<EventsFacade, EventDetailDTO, int> {

        // Injected services

        [Bind(Direction.None)]
        public BaseListsFacade BaseListsFacade { get; set; }

        [Bind(Direction.None)]
        public IUploadedFileStorage Storage { get; set; }

        // Page infrastructire

        protected override int ItemId => Convert.ToInt32(Context.Parameters["EventId"]);

        protected override string ContinuePageRouteName => "AdminEvents";

        protected override string GetPageTitle() => $"Editace akce {this.Item.Name}";

        // Binding properties

        [Bind(Direction.ServerToClientFirstRequest)]
        public List<UserBasicDTO> Users => this.BaseListsFacade.GetOrganizerOrAdminUsers();

        [Bind(Direction.ServerToClientFirstRequest)]
        public List<VenueBasicDTO> Venues => this.BaseListsFacade.GetVenues();

        [Bind(Direction.ServerToClientFirstRequest)]
        public List<TagDTO> Tags => this.BaseListsFacade.GetTags();

        [Bind(Direction.ServerToClientFirstRequest)]
        public List<AttachmentTypeDTO> AttachmentTypes => BaseListsFacade.GetAttachmentTypes();

        // Attachment upload

        public UploadedFilesCollection AttachmentUploads { get; set; } = new UploadedFilesCollection();

        public async Task UploadAttachments() {
            foreach (var file in AttachmentUploads.Files) {
                using (var stream = Storage.GetFile(file.FileId)) {
                    var attachment = await Facade.UploadAttachmentAsync(ItemId, file.FileName, stream);
                    Item.Attachments.Add(attachment);
                }
            }
            AttachmentUploads.Clear();
        }

        public void RemoveAttachment(AttachmentDTO attachment) {
            Item.Attachments.Remove(attachment);
        }
    }
}

