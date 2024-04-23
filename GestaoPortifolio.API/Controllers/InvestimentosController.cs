using GestaoPortfolio.Domain.Interfaces;
using GestaoPortfolio.Domain.Interfaces.Facades;
using GestaoPortfolio.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace GestaoPortifolio.API.Controllers
{
    public class InvestimentosController : BaseController
    {
        private readonly IInvestimentosFacade investimentosFacade;
        private readonly IInvestimentosRepository investimentosRepository;

        public InvestimentosController(IInvestimentosFacade investimentosFacade, IInvestimentosRepository investimentosRepository, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            this.investimentosFacade = investimentosFacade;
            this.investimentosRepository = investimentosRepository;
        }

        [HttpGet]
        [Route("listar")]
        public async Task<IActionResult> Getinvestimentos([FromQuery] Investimentos investimentos)
        {
            var resultado = await investimentosRepository.Listar(investimentos);
            return Ok(resultado);
        }

        [HttpPost]
        [Route("incluir")]
        public async Task<IActionResult> PostCarteira([FromBody] Investimentos investimentos)
        {
            var resultado = await investimentosFacade.IncluirPosicao(investimentos);
            return Ok(resultado);
        }

        [HttpPut]
        [Route("alterar")]
        public async Task<IActionResult> PutCarteira([FromBody] Investimentos investimentos)
        {
            var resultado = await investimentosFacade.AlterarPosicao(investimentos);
            return Ok(resultado);
        }

        [HttpDelete]
        [Route("excluir/{idPosicao}")]
        public async Task<IActionResult> DeleteCarteira([FromRoute] int idPosicao)
        {
            await investimentosFacade.ExcluirPosicao(idPosicao);
            return Ok();
        }
    }
}
