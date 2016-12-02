using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Altairis.NemesisEvents.DAL;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFrameworkCore;

namespace Altairis.NemesisEvents.BL.Queries
{
    public abstract class AppQueryBase<TResult> : EntityFrameworkQuery<TResult>
    {

        public new NemesisEventsContext Context => (NemesisEventsContext) base.Context;

        public AppQueryBase(IUnitOfWorkProvider provider) : base(provider)
        {
        }
    }
}
