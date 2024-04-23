using GestaoPortfolio.Domain.Interfaces;
using GestaoPortfolio.Domain.Models;
using GestaoPortfolio.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace GestaoPortfolio.Infra.Repository
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<TEntity> _entities;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
            _entities = context.Set<TEntity>();
        }

        public TEntity GetById(object id)
        {
            return  _entities.Find(id);
        }

        public async Task<TEntity> GetByIdAsync(object id)
        {
            return await _entities.FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            try
            {
                return await _entities.ToListAsync();
            } catch (Exception ex) { 
                 var erro = ex.InnerException.Message;
                return await _entities.ToListAsync();
            }  
        }

        public async Task<TEntity> Insert(TEntity obj)
        {
            await _entities.AddAsync(obj);
            try {
                await _context.SaveChangesAsync();
            } catch (Exception ex)
            {
                string erro = ex.Message;
            }

            return obj;
        }

        public async Task<TEntity> Update(TEntity obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }catch (Exception ex)
            {
                var erro = ex.InnerException.Message;
            }

            return obj;
        }

        public async Task Delete(int id)
        {
            _context.Set<TEntity>().Remove(await GetByIdAsync(id));
            try
            {
                await _context.SaveChangesAsync();
            } catch (Exception ex)
            {
                string erro = ex.Message;
            }

        }
    }
}
