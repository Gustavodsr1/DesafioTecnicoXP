using GestaoPortfolio.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoPortfolio.Domain.Interfaces.Services
{
    public interface IIncluirPosicaoService
    {
        Task<Investimentos> IncluirPosicao(Investimentos carteira);
    }
}
