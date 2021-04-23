using AuthServer.Core.Repositories;
using AuthServer.Core.Services;
using AuthServer.Core.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AuthServer.Service.Services
{
    public class GenericService<TEntity, TDTOs> : IGenericService<TEntity, TDTOs> where TEntity : class where TDTOs : class
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<TEntity> _genericRepository;

        public GenericService(IUnitOfWork unitOfWork, IGenericRepository<TEntity> genericRepository)
        {
            _unitOfWork = unitOfWork;
            _genericRepository = genericRepository;
        }

        public async Task<Response<TDTOs>> AddAsync(TDTOs entity)
        {
            var addedEntity = ObjectMapper.Mapper.Map<TEntity>(entity);
            await _genericRepository.AddAsync(addedEntity);
            await _unitOfWork.CommitAsync();
            var addedDTO = ObjectMapper.Mapper.Map<TDTOs>(addedEntity);
            return Response<TDTOs>.Success(addedDTO, 200);
        }

        public async Task<Response<IEnumerable<TDTOs>>> GetAllAsync()
        {
            var entities = ObjectMapper.Mapper.Map<List<TDTOs>>(await _genericRepository.GetAllAsync());
            return Response<IEnumerable<TDTOs>>.Success(entities, 200);
        }

        public async Task<Response<TDTOs>> GetByIdAsync(int id)
        {
            var entity = await _genericRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return Response<TDTOs>.Fail("Id Not Found", 404, true);
            }
            return Response<TDTOs>.Success(ObjectMapper.Mapper.Map<TDTOs>(entity), 200);
        }

        public async Task<Response<NoDataDTOs>> RemoveAsync(int id)
        {
            var entity = await _genericRepository.GetByIdAsync(id);
            if (entity == null)
            {
                return Response<NoDataDTOs>.Fail("Id Not Found", 404, true);
            }
            _genericRepository.Remove(entity);
            await _unitOfWork.CommitAsync();
            return Response<NoDataDTOs>.Success(204);
        }

        public async Task<Response<NoDataDTOs>> UpdateAsync(TDTOs entity, int id)
        {
            var isExistEntity = await _genericRepository.GetByIdAsync(id);
            if (isExistEntity == null)
            {
                return Response<NoDataDTOs>.Fail("Id Not Found", 404, true);
            }

            var updateEntity = ObjectMapper.Mapper.Map<TEntity>(entity);
            _genericRepository.Update(updateEntity);
            await _unitOfWork.CommitAsync();
            return Response<NoDataDTOs>.Success(204);
        }

        public async Task<Response<IEnumerable<TDTOs>>> WhereAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var entities = _genericRepository.Where(predicate);
            return Response<IEnumerable<TDTOs>>.Success(ObjectMapper.Mapper.Map<IEnumerable<TDTOs>>(await entities.ToListAsync()), 200);
        }
    }
}
