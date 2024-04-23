using GestaoPortfolio.Domain.Interfaces;
using GestaoPortfolio.Domain.Interfaces.Services;
using GestaoPortfolio.Domain.Models;
using static GestaoPortfolio.Domain.Models.Enum.EventoEnum;

namespace GestaoPortfolio.Application.Services
{
    public class IncluirOperacaoService : IIncluirOperacaoService
    {
        private readonly IOperacaoRepository operacaoRepository;

        public IncluirOperacaoService(IOperacaoRepository operacaoRepository)
        {
            this.operacaoRepository = operacaoRepository;
        }

        public async Task<Operacao> Incluir(Operacao operacao)
        {
            await operacaoRepository.Insert(operacao);

            return operacao;
        }
    }
}
