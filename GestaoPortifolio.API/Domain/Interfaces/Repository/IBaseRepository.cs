using GestaoPortfolio.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoPortfolio.Domain.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        TEntity GetById(object id);
        Task<TEntity> GetByIdAsync(object id);
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> Insert(TEntity obj);
        Task<TEntity> Update(TEntity obj);
        Task Delete(int id);
    }
}
