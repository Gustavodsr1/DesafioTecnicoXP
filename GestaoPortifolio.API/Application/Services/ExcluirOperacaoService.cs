using GestaoPortfolio.Domain.Interfaces;
using GestaoPortfolio.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoPortfolio.Application.Services
{
    public class ExcluirOperacaoService : IExcluirOperacaoService
    {
        private readonly IOperacaoRepository operacaoRepository;

        public ExcluirOperacaoService(IOperacaoRepository operacaoRepository)
        {
            this.operacaoRepository = operacaoRepository;
        }

        public async Task Excluir(int id)
        {
            await operacaoRepository.Delete(id);
        }
    }
}
