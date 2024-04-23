using GestaoPortfolio.Domain.Interfaces;
using GestaoPortfolio.Domain.Interfaces.Services;
using GestaoPortfolio.Domain.Models;

namespace GestaoPortfolio.Application.Services
{
    public class IncluirOfertaService : IIncluirOfertaService
    {
        private readonly IOfertaRepository ofertaRepository;

        public IncluirOfertaService(IOfertaRepository ofertaRepository)
        {
            this.ofertaRepository = ofertaRepository;
        }

        public async Task<Oferta> Incluir(Oferta oferta)
        {
            await ofertaRepository.Insert(oferta);

            return oferta;
        }
    }
}
