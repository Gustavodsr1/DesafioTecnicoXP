using GestaoPortfolio.Domain.Interfaces;
using GestaoPortfolio.Domain.Models;
using GestaoPortfolio.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace GestaoPortfolio.Infra.Repository
{
    public class ClienteRepository : BaseRepository<Cliente>, IClienteRepository
    {
        public ClienteRepository(AppDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<Cliente>> Listar(Cliente cliente)
        {
            var linq = from e in _entities select e;

            if (cliente.Id > 0)
            {
                linq = linq.Where(x => x.Id == cliente.Id);
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(cliente.Nome))
                {
                    linq = linq.Where(x => cliente.Nome == x.Nome);
                }
                else
                {
                    await GetAll();
                }
            }

            return await linq.ToListAsync();
        }
    }
}
