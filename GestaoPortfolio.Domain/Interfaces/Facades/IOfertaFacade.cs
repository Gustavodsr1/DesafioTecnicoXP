using GestaoPortfolio.Domain.Models;

namespace GestaoPortfolio.Domain.Interfaces.Facades
{
    public interface IOfertaFacade
    {
        Task<Oferta> Inserir(Oferta oferta);
        Task<Oferta> Alterar(Oferta oferta);
        Task Excluir(int id);
    }
}
