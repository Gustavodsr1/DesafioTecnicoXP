using GestaoPortfolio.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoPortfolio.Domain.Interfaces
{
    public interface IInvestimentosRepository : IBaseRepository<Investimentos>
    {
        Task<IEnumerable<Investimentos>> Listar(Investimentos carteira);
        Task<IEnumerable<Investimentos>> ListarVencimentos(DateTime dataVencimento);
        Investimentos BuscarCarteiraCliente(int idOferta, int idCliente);
    }
}
