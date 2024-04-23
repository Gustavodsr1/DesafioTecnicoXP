using GestaoPortfolio.Domain.Interfaces;
using GestaoPortfolio.Domain.Interfaces.Services;
using GestaoPortfolio.Domain.Models;

namespace GestaoPortfolio.Application.Services
{
    public class IncluirPosicaoService : IIncluirPosicaoService
    {
        private readonly IInvestimentosRepository carteiraRepository;

        public IncluirPosicaoService(IInvestimentosRepository carteiraRepository)
        {
            this.carteiraRepository = carteiraRepository;
        }

        public async Task<Investimentos> IncluirPosicao(Investimentos carteira)
        {
            await carteiraRepository.Insert(carteira);

            return carteira;
        }
    }
}
