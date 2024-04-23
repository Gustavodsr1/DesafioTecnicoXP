using GestaoPortfolio.Domain.Interfaces;
using GestaoPortfolio.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoPortfolio.Application.Services
{
    public class ExcluirClienteService : IExcluirClienteService
    {
        private readonly IClienteRepository clienteRepository;

        public ExcluirClienteService(IClienteRepository clienteRepository)
        {
            this.clienteRepository = clienteRepository;
        }

        public async Task Excluir(int id)
        {
            await clienteRepository.Delete(id);
        }
    }
}
