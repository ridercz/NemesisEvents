using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.Services.Facades;
using System;
using DotVVM.Framework.Controls;
using Riganti.Utils.Infrastructure;

namespace Altairis.NemesisEvents.BL.Facades {
    public class AppFilteredCrudFacadeBase<TEntity, TKey, TListDTO, TDetailDTO, TFilterDTO>
        : FilteredCrudFacadeBase<TEntity, TKey, TListDTO, TDetailDTO, TFilterDTO>,
        ICrudFacade<TDetailDTO, TKey>, IDataSourceFacade<TListDTO>
        where TEntity : IEntity<TKey>
        where TDetailDTO : IEntity<TKey> {

        public AppFilteredCrudFacadeBase(Func<IFilteredQuery<TListDTO, TFilterDTO>> queryFactory, IRepository<TEntity, TKey> repository, IEntityDTOMapper<TEntity, TDetailDTO> mapper) : base(queryFactory, repository, mapper) {
        }

        public virtual void FillDataSet(GridViewDataSet<TListDTO> dataSet) {
            if (dataSet == null) throw new ArgumentNullException(nameof(dataSet));

            using (var uow = this.UnitOfWorkProvider.Create()) {
                var q = this.QueryFactory();
                dataSet.LoadFromQuery(q);
            }
        }

    }
}