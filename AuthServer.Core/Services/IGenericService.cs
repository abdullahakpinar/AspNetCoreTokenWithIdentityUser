using SharedLibrary.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AuthServer.Core.Services
{
   public interface IGenericService<TEntity, TDTOs> where TEntity : class where TDTOs : class
    {
        Task<Response<IEnumerable<TDTOs>>> GetAllAsync();
        Task<Response<TDTOs>> GetByIdAsync(int id);
        Task<Response<IEnumerable<TDTOs>>> WhereAsync(Expression<Func<TEntity, bool>> predicate);
        Task<Response<TDTOs>> AddAsync(TDTOs entity);
        Task<Response<NoDataDTOs>> RemoveAsync(int id);
        Task<Response<NoDataDTOs>> UpdateAsync(TDTOs entity, int id);
    }
}
