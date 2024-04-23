using GestaoPortfolio.Domain.Models;

namespace GestaoPortfolio.Domain.Interfaces.Facades
{
    public interface IClienteFacade
    {
        Task<Cliente> Inserir(Cliente cliente);
        Task<Cliente> Alterar(Cliente cliente);
        Task Excluir(int id);
    }
}
