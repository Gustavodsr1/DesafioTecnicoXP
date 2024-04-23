using GestaoPortfolio.Domain.Interfaces;
using GestaoPortfolio.Domain.Models;
using GestaoPortfolio.Infra.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace GestaoPortfolio.Infra.Repository
{
    public class ProdutoRepository : BaseRepository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(AppDbContext context) 
            : base(context)
        {
        }

        public async Task<IEnumerable<Produto>> Listar(Produto produto)
        {
            var linq = from e in _entities select e;

            if (produto.Codigo > 0)
            {
                linq = linq.Where(x => x.Codigo == produto.Codigo);
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(produto.Nome))
                {
                    linq = linq.Where(x => produto.Nome == x.Nome);
                } else if (!string.IsNullOrWhiteSpace(produto.Descricao))
                {
                    linq = linq.Where(x => produto.Descricao == x.Descricao);
                } else
                {
                    await GetAll();
                }
            }

            return await linq.ToListAsync();
        }
    }
}
