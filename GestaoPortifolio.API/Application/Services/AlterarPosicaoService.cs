using GestaoPortfolio.Domain.Interfaces;
using GestaoPortfolio.Domain.Interfaces.Services;
using GestaoPortfolio.Domain.Models;

namespace GestaoPortfolio.Application.Services
{
    public class AlterarPosicaoService : IAlterarPosicaoService
    {
        private readonly IInvestimentosRepository carteiraRepository;

        public AlterarPosicaoService(IInvestimentosRepository carteiraRepository)
        {
            this.carteiraRepository = carteiraRepository;
        }

        public async Task<Investimentos> AlterarPosicao(Investimentos carteira)
        {
            return await carteiraRepository.Update(carteira);
        }
    }
}
