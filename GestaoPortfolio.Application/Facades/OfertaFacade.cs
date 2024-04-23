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
    public class OfertaFacade : IOfertaFacade
    {
        private readonly IIncluirOfertaService incluirOfertaService;
        private readonly IAlterarOfertaService alterarOfertaService;
        private readonly IExcluirOfertaService excluirOfertaService;

        public OfertaFacade(IIncluirOfertaService incluirOfertaService, IAlterarOfertaService alterarOfertaService, IExcluirOfertaService excluirOfertaService)
        {
            this.incluirOfertaService = incluirOfertaService;
            this.alterarOfertaService = alterarOfertaService;
            this.excluirOfertaService = excluirOfertaService;
        }

        public async Task<Oferta> Alterar(Oferta oferta)
        {
            return await alterarOfertaService.Alterar(oferta);
        }

        public async Task Excluir(int id)
        {
            await excluirOfertaService.Excluir(id);
        }

        public async Task<Oferta> Inserir(Oferta oferta)
        {
            return await incluirOfertaService.Incluir(oferta);
        }
    }
}
