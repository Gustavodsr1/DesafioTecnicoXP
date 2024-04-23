using GestaoPortfolio.Domain.Models;

namespace GestaoPortfolio.Domain.Interfaces.Facades
{
    public interface IInvestimentosFacade
    {
        Task<Investimentos> IncluirPosicao(Investimentos carteira);
        Task<Investimentos> AlterarPosicao(Investimentos carteira);
        Task ExcluirPosicao(int id);
    }
}
