using GestaoPortfolio.Domain.Interfaces;
using GestaoPortfolio.Domain.Interfaces.Services;
using GestaoPortfolio.Domain.Models;

namespace GestaoPortfolio.Application.Services
{
    public class AlterarOfertaService : IAlterarOfertaService
    {
        private readonly IOfertaRepository ofertaRepository;

        public AlterarOfertaService(IOfertaRepository ofertaRepository)
        {
            this.ofertaRepository = ofertaRepository;
        }

        public async Task<Oferta> Alterar(Oferta oferta)
        {
            return await ofertaRepository.Update(oferta);
        }
    }
}
