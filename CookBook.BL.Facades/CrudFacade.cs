using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CookBook.BL.Repository;
using CookBook.BL.Repository.Base;
using CookBook.DAL.Entities.Base;
using CookBook.Shared.Interfaces;

namespace CookBook.BL.Facades
{
    public class CrudFacade<TEntity, TListDTO, TDetailDTO>
        where TEntity : EntityBase, new()
        where TListDTO : IId
        where TDetailDTO : class, IId
    {
        private readonly IMapper _mapper;
        private readonly RepositoryBase<TEntity> _repository;
        private readonly UnitOfWork _unitOfWork;

        public CrudFacade(RepositoryBase<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = repository.UnitOfWork;
        }

        public IEnumerable<TListDTO> GetList()
        {
            return _repository.GetAll().Select(i => _mapper.Map<TListDTO>(i));
        }

        public TDetailDTO GetDetail(Guid id)
        {
            var entity = _repository.GetById(id);
            if (entity == null) return null;

            return _mapper.Map<TDetailDTO>(entity);
        }

        public TDetailDTO Save(TDetailDTO detailDTO)
        {
            var entity = _mapper.Map<TEntity>(detailDTO);

            if (detailDTO.Id == Guid.Empty)
                _repository.Insert(entity);
            else
                _repository.Update(entity);

            _unitOfWork.Commit();

            return _mapper.Map<TDetailDTO>(entity);
        }

        public void Delete(TDetailDTO detailDTO)
        {
            this._repository.Delete(detailDTO.Id);
            _unitOfWork.Commit();
        }

        public void Delete(Guid id)
        {
            this._repository.Delete(id);
            _unitOfWork.Commit();
        }

        public TDetailDTO InitializeNew()
        {
            return _mapper.Map<TDetailDTO>(_repository.InitializeNew());
        }
    }
}