using GestaoPortfolio.Domain.Interfaces;
using GestaoPortfolio.Domain.Models;
using GestaoPortfolio.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace GestaoPortfolio.Infra.Repository
{
    public class OperacaoRepository : BaseRepository<Operacao>, IOperacaoRepository
    {
        public OperacaoRepository(AppDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<ExtratoProduto>> ListarExtrato(Operacao operacao)
        {
            var linq = from e in _entities 
                       select new ExtratoProduto()
                       {
                           CodigoOferta = e.CodigoOferta,
                           CodigoProduto = e.CodigoProduto,
                           IdCliente = e.IdCliente,
                           IdOperacao = e.IdOperacao,
                           QuantidadeDisponivelEstoque = e.QuantidadeDisponivelEstoque,
                           TipoEvento = e.Evento,
                           QuantidadeMovimentada = e.QuantidadeOperacao,
                           ValorOperacao = e.ValorTotalOperacao,
                           DataMovimentacao = e.DataOperacao
                       };

            if (operacao.IdCliente > 0)
            {
                linq = linq.Where(x => x.IdCliente == operacao.IdCliente);
            }
            else if (operacao.CodigoProduto > 0) {
                linq = linq.Where(x => x.CodigoProduto == operacao.CodigoProduto);
            }            

            return await linq.ToListAsync();
        }

        public async Task<IEnumerable<Operacao>> Listar(Operacao operacao)
        {
            var linq = from e in _entities select e;

            if (operacao.IdOperacao > 0)
            {
                linq = linq.Where(x => x.IdCliente == operacao.IdCliente);
            }
            else
            {
                await GetAll();
            }

            return await linq.ToListAsync();
        }
    }
}
