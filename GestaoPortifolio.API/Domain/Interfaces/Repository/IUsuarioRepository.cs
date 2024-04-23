using GestaoPortfolio.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoPortfolio.Domain.Interfaces
{
    public interface IUsuarioRepository : IBaseRepository<Usuario>
    {
        Task<Usuario> GetByEmail(string email);
    }
}
