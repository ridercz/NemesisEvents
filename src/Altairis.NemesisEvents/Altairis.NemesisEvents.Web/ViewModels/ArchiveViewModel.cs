using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.Framework.ViewModel;
using Altairis.NemesisEvents.BL.Facades;
using System.Threading.Tasks;
using Altairis.NemesisEvents.BL.DTO;
using DotVVM.Framework.Controls;

namespace Altairis.NemesisEvents.Web.ViewModels
{
	public class ArchiveViewModel : MasterPageViewModel
	{
        private PublicEventsFacade facade;

        public ArchiveViewModel(PublicEventsFacade facade)
        {
            this.facade = facade;
        }


        public GridViewDataSet<PublicArchiveEventDTO> ArchiveEvents { get; set; } = new GridViewDataSet<PublicArchiveEventDTO>()
        {
            PagingOptions = {
                PageSize = 40
            },
            SortingOptions = {
                SortExpression = nameof(PublicArchiveEventDTO.DateBegin),
                SortDescending = true
            }
        };


        public override Task PreRender()
        {
            facade.FillArchiveEvents(ArchiveEvents);
            return base.PreRender();
        }

    }
}

