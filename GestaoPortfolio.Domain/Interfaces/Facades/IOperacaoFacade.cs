using GestaoPortfolio.Domain.Models;

namespace GestaoPortfolio.Domain.Interfaces.Facades
{
    public interface IOperacaoFacade
    {
        Task<Operacao> Inserir(Operacao operacao);
        Task<Operacao> Alterar(Operacao operacao);
        Task Excluir(int id);
    }
}
