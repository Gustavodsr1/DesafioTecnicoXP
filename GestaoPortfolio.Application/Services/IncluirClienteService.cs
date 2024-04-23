using GestaoPortfolio.Domain.Interfaces;
using GestaoPortfolio.Domain.Interfaces.Services;
using GestaoPortfolio.Domain.Models;

namespace GestaoPortfolio.Application.Services
{
    public class IncluirClienteService : IIncluirClienteService
    {
        private readonly IClienteRepository clienteRepository;

        public IncluirClienteService(IClienteRepository clienteRepository)
        {
            this.clienteRepository = clienteRepository;
        }

        public async Task<Cliente> Incluir(Cliente cliente)
        {
            await clienteRepository.Insert(cliente);

            return cliente;
        }
    }
}
