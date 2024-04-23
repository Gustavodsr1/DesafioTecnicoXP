using GestaoPortfolio.Application.Services;
using GestaoPortfolio.Domain.Interfaces.Facades;
using GestaoPortfolio.Domain.Interfaces.Services;
using GestaoPortfolio.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoPortfolio.Application.Facades
{
    public class CarteiraFacade : IInvestimentosFacade
    {
        private readonly IIncluirPosicaoService incluirPosicaoService;
        private readonly IAlterarPosicaoService alterarPosicaoService;
        private readonly IExcluirPosicaoService excluirPosicaoService;

        public CarteiraFacade(IIncluirPosicaoService incluirPosicaoService, IAlterarPosicaoService alterarPosicaoService, IExcluirPosicaoService excluirPosicaoService)
        {
            this.incluirPosicaoService = incluirPosicaoService;
            this.alterarPosicaoService = alterarPosicaoService;
            this.excluirPosicaoService = excluirPosicaoService;
        }

        public async Task<Investimentos> AlterarPosicao(Investimentos carteira)
        {
            return await alterarPosicaoService.AlterarPosicao(carteira);
        }

        public async Task ExcluirPosicao(int id)
        {
            await excluirPosicaoService.ExcluirPosicao(id);
        }

        public async Task<Investimentos> IncluirPosicao(Investimentos carteira)
        {
            return await incluirPosicaoService.IncluirPosicao(carteira);
        }
    }
}
