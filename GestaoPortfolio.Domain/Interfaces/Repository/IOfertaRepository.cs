using GestaoPortfolio.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoPortfolio.Domain.Interfaces
{
    public interface IOfertaRepository : IBaseRepository<Oferta>
    {
        Task<IEnumerable<Oferta>> Listar(Oferta oferta);
    }
}
