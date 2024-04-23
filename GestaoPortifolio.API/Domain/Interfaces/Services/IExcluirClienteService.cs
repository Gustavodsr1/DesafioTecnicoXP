using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoPortfolio.Domain.Interfaces.Services
{
    public interface IExcluirClienteService
    {
        Task Excluir(int id);
    }
}
