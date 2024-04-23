using GestaoPortfolio.Domain.Interfaces;
using GestaoPortfolio.Domain.Models;
using GestaoPortfolio.Infra.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace GestaoPortfolio.Infra.Repository
{
    public class OfertaRepository : BaseRepository<Oferta>, IOfertaRepository
    {
        public OfertaRepository(AppDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<Oferta>> Listar(Oferta oferta)
        {
            var linq = from e in _entities select e;

            if (oferta.CodigoOferta > 0)
            {
                linq = linq.Where(x => x.CodigoOferta == oferta.CodigoOferta);
            } else
            {
               await GetAll();
            }

            return await linq.ToListAsync();
        }
    }
}
