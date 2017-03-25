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

namespace Altairis.NemesisEvents.Web.ViewModels.Admin
{
	public class EventsEditViewModel : EditViewModelBase<EventsFacade, EventDetailDTO, int>
	{
	    private readonly BaseListsFacade baseListsFacade;
	    private readonly IUploadedFileStorage storage;
	    protected override int ItemId => Convert.ToInt32(Context.Parameters["EventId"]);
	    protected override string ContinuePageRouteName => "AdminEvents";
	    protected override string GetPageTitle() => $"Editace akce {this.Item.Name}";


	    public EventsEditViewModel(BaseListsFacade baseListsFacade, IUploadedFileStorage storage)
	    {
	        this.baseListsFacade = baseListsFacade;
	        this.storage = storage;
	    }


        [Bind(Direction.ServerToClientFirstRequest)]
	    public List<UserBasicDTO> Users => baseListsFacade.GetOrganizerOrAdminUsers();

        [Bind(Direction.ServerToClientFirstRequest)]
        public List<VenueBasicDTO> Venues => baseListsFacade.GetVenues();

        [Bind(Direction.ServerToClientFirstRequest)]
        public List<TagDTO> Tags => baseListsFacade.GetTags();

        [Bind(Direction.ServerToClientFirstRequest)]
        public List<AttachmentTypeDTO> AttachmentTypes => baseListsFacade.GetAttachmentTypes();


	    public UploadedFilesCollection AttachmentUploads { get; set; } = new UploadedFilesCollection();

	    public async Task UploadAttachments()
	    {
	        foreach (var file in AttachmentUploads.Files)
	        {
	            using (var stream = storage.GetFile(file.FileId))
	            {
	                var attachment = await Facade.UploadAttachmentAsync(ItemId, file.FileName, stream);
                    Item.Attachments.Add(attachment);
	            }
	        }
	        AttachmentUploads.Clear();
	    }

	    public void RemoveAttachment(AttachmentDTO attachment)
	    {
	        Item.Attachments.Remove(attachment);
	    }
    }
}

