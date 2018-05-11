using System;
using System.Collections.Generic;
using AutoMapper;
using CookBook.BL.Facades;
using CookBook.DAL.Entities.Base;
using CookBook.Shared.Interfaces;

namespace CookBook.UI.WPFApp.Adapters
{
    public class CrudFacadeAdapter<TEntity, TListDTO, TDetailDTO, TListModel, TDetailModel>
        where TEntity : EntityBase, new()
        where TListDTO : IId
        where TDetailDTO : class, IId
        where TListModel : new()
        where TDetailModel : new()
    {
        private readonly CrudFacade<TEntity, TListDTO, TDetailDTO> _crudFacade;
        private readonly IMapper _mapper;

        public CrudFacadeAdapter(CrudFacade<TEntity, TListDTO, TDetailDTO> crudFacade, IMapper mapper)
        {
            _crudFacade = crudFacade;
            _mapper = mapper;
        }

        public IEnumerable<TListModel> GetList()
        {
            return _mapper.Map<IEnumerable<TListModel>>(_crudFacade.GetList());
        }

        public TDetailModel GetDetail(Guid id)
        {
            return _mapper.Map<TDetailModel>(_crudFacade.GetDetail(id));
        }

        public TDetailModel Save(TDetailModel detailModel)
        {
            var detailDTO = _mapper.Map<TDetailDTO>(detailModel);
            detailDTO = _crudFacade.Save(detailDTO);
            return _mapper.Map<TDetailModel>(detailDTO);
        }

        public void Delete(TDetailModel detailModel)
        {
            _crudFacade.Delete(_mapper.Map<TDetailDTO>(detailModel));
        }

        public void Delete(Guid id)
        {
            _crudFacade.Delete(id);
        }

        public TDetailModel InitializeNew()
        {
            return _mapper.Map<TDetailModel>(_crudFacade.InitializeNew());
        }
    }
}