using GestaoPortfolio.Domain.Interfaces;
using GestaoPortfolio.Domain.Models;
using GestaoPortfolio.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace GestaoPortfolio.Infra.Repository
{
    public class CarteiraRepository : BaseRepository<Investimentos>, IInvestimentosRepository
    {
        public CarteiraRepository(AppDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<Investimentos>> Listar(Investimentos carteira)
        {
            var linq = from e in _entities select e;

            if (carteira.IdCliente > 0)
            {
                linq = linq.Where(x => x.IdCliente == carteira.IdCliente);
            }
            else
            {
                await GetAll();
            }

            return await linq.ToListAsync();
        }
        public Investimentos Carteira { get; set; }
        public async Task<IEnumerable<Investimentos>> ListarVencimentos(DateTime dataVencimento)
        {
            var linq = from e in _entities
                       where e.DataVencimento.Date == dataVencimento.Date
                       select e;

            return await linq.ToListAsync();
        }

        public Investimentos BuscarCarteiraCliente(int idOferta, int idCliente)
        {
            Investimentos carteira = new Investimentos();

            var linq = from e in _entities select e;
            linq = linq.Where(x => x.IdCliente == idCliente && x.CodigoOferta == idOferta);
            carteira = linq.FirstOrDefault();
            return carteira;
        }
    }
}
