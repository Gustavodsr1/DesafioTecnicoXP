using GestaoPortfolio.Domain.Interfaces;
using GestaoPortfolio.Domain.Interfaces.Services;
using GestaoPortfolio.Domain.Models;

namespace GestaoPortfolio.Application.Services
{
    public class AlterarClienteService : IAlterarClienteService
    {
        private readonly IClienteRepository clienteRepository;

        public AlterarClienteService(IClienteRepository clienteRepository)
        {
            this.clienteRepository = clienteRepository;
        }

        public async Task<Cliente> Alterar(Cliente cliente)
        {
            return await clienteRepository.Update(cliente);
        }
    }
}
