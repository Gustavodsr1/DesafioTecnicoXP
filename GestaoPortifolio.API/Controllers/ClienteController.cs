using GestaoPortfolio.Domain.Interfaces;
using GestaoPortfolio.Domain.Interfaces.Facades;
using GestaoPortfolio.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace GestaoPortifolio.API.Controllers
{
    public class ClienteController : BaseController
    {
        private readonly IClienteFacade clienteFacade;
        private readonly IClienteRepository clienteRepository;

        public ClienteController(IClienteFacade clienteFacade, IClienteRepository clienteRepository, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            this.clienteFacade = clienteFacade;
            this.clienteRepository = clienteRepository;
        }

        [HttpGet]
        [Route("listar")]
        public async Task<IActionResult> GetCliente([FromQuery] Cliente cliente)
        {
            var resultado = await clienteRepository.Listar(cliente);
            return Ok(resultado);
        }

        [HttpPost]
        [Route("incluir")]
        public async Task<IActionResult> PostCliente([FromBody] Cliente cliente)
        {
            var resultado = await clienteFacade.Inserir(cliente);
            return Ok(resultado);
        }

        [HttpPut]
        [Route("alterar")]
        public async Task<IActionResult> PutCliente([FromBody] Cliente cliente)
        {
            var resultado = await clienteFacade.Alterar(cliente);
            return Ok(resultado);
        }

    }
}
