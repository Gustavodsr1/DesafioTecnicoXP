using GestaoPortfolio.Domain.Interfaces;
using GestaoPortfolio.Domain.Interfaces.Services;
using GestaoPortfolio.Domain.Models;

namespace GestaoPortfolio.Application.Services
{
    public class AlterarOperacaoService : IAlterarOperacaoService
    {
        private readonly IOperacaoRepository operacaoRepository;

        public AlterarOperacaoService(IOperacaoRepository operacaoRepository)
        {
            this.operacaoRepository = operacaoRepository;
        }

        public async Task<Operacao> Alterar(Operacao operacao)
        {
            return await operacaoRepository.Update(operacao);
        }
    }
}
