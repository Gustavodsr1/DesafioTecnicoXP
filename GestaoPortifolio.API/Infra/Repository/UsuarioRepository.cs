using GestaoPortfolio.Domain.Interfaces;
using GestaoPortfolio.Domain.Models;
using GestaoPortfolio.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace GestaoPortfolio.Infra.Repository
{
    internal class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(AppDbContext context) 
            : base(context)
        {
        }

        public async Task<Usuario> GetByEmail(string email)
        {
            var linq = from e in _entities where e.Email == email select e;

            return await linq.FirstOrDefaultAsync();
        }
    }
}
