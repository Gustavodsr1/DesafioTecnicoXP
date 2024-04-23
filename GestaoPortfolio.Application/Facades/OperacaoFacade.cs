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
    public class OperacaoFacade : IOperacaoFacade
    {
        private readonly IIncluirOperacaoService incluirOperacaoService;
        private readonly IAlterarOperacaoService alterarOperacaoService;
        private readonly IExcluirOperacaoService excluirOperacaoService;

        public OperacaoFacade(IIncluirOperacaoService incluirOperacaoService, IAlterarOperacaoService alterarOperacaoService, IExcluirOperacaoService excluirOperacaoService)
        {
            this.incluirOperacaoService = incluirOperacaoService;
            this.alterarOperacaoService = alterarOperacaoService;
            this.excluirOperacaoService = excluirOperacaoService;
        }

        public async Task<Operacao> Alterar(Operacao operacao)
        {
            return await alterarOperacaoService.Alterar(operacao);
        }

        public async Task Excluir(int id)
        {
            await excluirOperacaoService.Excluir(id);
        }

        public async Task<Operacao> Inserir(Operacao operacao)
        {
            return await incluirOperacaoService.Incluir(operacao);
        }
    }
}
