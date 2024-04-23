using GestaoPortfolio.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoPortfolio.Domain.Interfaces
{
    public interface IProdutoRepository : IBaseRepository<Produto>
    {
        Task<IEnumerable<Produto>> Listar(Produto produto);
    }
}
