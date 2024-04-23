using GestaoPortfolio.Domain.Models;

namespace GestaoPortfolio.Domain.Interfaces.Facades
{
    public interface IProdutoFacade
    {
        Task<Produto> Inserir(Produto produto);
        Task<Produto> Alterar(Produto produto);
        Task Excluir(int id);
    }
}
