using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Service.Exceptions;
using System.Linq.Expressions;

namespace NLayer.Service.Services
{
    public class Services<InT,OutT> : IServices<InT, OutT> where InT : BaseEntity where OutT : class
    {
       
        private readonly IGenericRepository<InT> _repository;
        protected readonly IMapper _mapper;
        protected readonly IUnitOfWork _unitOfWork;

        public Services(IUnitOfWork unitOfWork, IMapper mapper, IGenericRepository<InT> repository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<CustomResponseDTO<OutT>> AddAsync(OutT entity)
        {
            var NewEntity = _mapper.Map<InT>(entity);
            await _repository.AddAsync(NewEntity);
            await _unitOfWork.CommitAsync();
            return CustomResponseDTO<OutT>.Success(entity, StatusCodes.Status200OK);

        }

        public async Task<CustomResponseDTO<IEnumerable<OutT>>> AddRangeAsync(IEnumerable<OutT> entities)
        {
            var NewEntities= _mapper.Map<IEnumerable<InT>>(entities);
            await _repository.AddRangeAsync(NewEntities);
            await _unitOfWork.CommitAsync();
            return CustomResponseDTO<IEnumerable<OutT>>.Success(entities, StatusCodes.Status200OK);
        }

        public async Task<CustomResponseDTO<bool>> AnyAsync(Expression<Func<InT, bool>> expression)
        {
            var any= await _repository.AnyAsync(expression);
            return CustomResponseDTO<bool>.Success(any, StatusCodes.Status200OK);
        }

        public async Task<CustomResponseDTO<IEnumerable<OutT>>> GetAllAsync()
        {
            var entities= await _repository.GetAll().ToListAsync();
            var entitiesDto = _mapper.Map<IEnumerable<OutT>>(entities);
            return CustomResponseDTO<IEnumerable<OutT>>.Success(entitiesDto, StatusCodes.Status200OK);
        }

        public async Task<CustomResponseDTO<OutT>> GetByIdAsync(int id)
        {
            var entity=await _repository.GetByIdAsync(id);
            var entityDto=_mapper.Map<OutT>(entity);
            return CustomResponseDTO<OutT>.Success(entityDto,StatusCodes.Status200OK);
        }

        public async Task<CustomResponseDTO<NoContentDTO>> RemoveAsync(int id)
        {
            var entity= await _repository.GetByIdAsync(id);
            _repository.Remove(entity);
            await _unitOfWork.CommitAsync();
            return CustomResponseDTO<NoContentDTO>.Success(StatusCodes.Status204NoContent);
        }

        public async Task<CustomResponseDTO<NoContentDTO>> RemoveRangeAsync(IEnumerable<int> ids)
        {
            var entities= await _repository.Where(x=>ids.Contains(x.Id)).ToListAsync();
            _repository.RemoveRange(entities);
            await _unitOfWork.CommitAsync();
            return CustomResponseDTO<NoContentDTO>.Success(StatusCodes.Status204NoContent);
        }

        public async Task<CustomResponseDTO<NoContentDTO>> UpdateAsync(OutT entity)
        {
            var UpdateEntity = _mapper.Map<InT>(entity);
            _repository.Update(UpdateEntity);
            await _unitOfWork.CommitAsync();
            return CustomResponseDTO<NoContentDTO>.Success(StatusCodes.Status204NoContent);
        }

        public CustomResponseDTO<IQueryable<InT>> Where(Expression<Func<InT, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
