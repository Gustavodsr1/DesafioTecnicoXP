using GestaoPortfolio.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoPortfolio.Domain.Interfaces
{
    public interface IOperacaoRepository : IBaseRepository<Operacao>
    {
        Task<IEnumerable<Operacao>> Listar(Operacao operacao);
        Task<IEnumerable<ExtratoProduto>> ListarExtrato(Operacao operacao);
    }
}
