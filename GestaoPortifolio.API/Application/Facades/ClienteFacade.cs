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
    public class ClienteFacade : IClienteFacade
    {
        private readonly IIncluirClienteService incluirClienteService;
        private readonly IAlterarClienteService alterarClienteService;
        private readonly IExcluirClienteService excluirClienteService;

        public ClienteFacade(IIncluirClienteService incluirClienteService, IAlterarClienteService alterarClienteService, IExcluirClienteService excluirClienteService)
        {
            this.incluirClienteService = incluirClienteService;
            this.alterarClienteService = alterarClienteService;
            this.excluirClienteService = excluirClienteService;
        }

        public async Task<Cliente> Alterar(Cliente cliente)
        {
            return await alterarClienteService.Alterar(cliente);
        }

        public async Task Excluir(int id)
        {
            await excluirClienteService.Excluir(id);
        }

        public async Task<Cliente> Inserir(Cliente cliente)
        {
            return await incluirClienteService.Incluir(cliente);
        }
    }
}
