using GestaoPortfolio.Domain.Interfaces;
using GestaoPortfolio.Domain.Interfaces.Facades;
using GestaoPortfolio.Domain.Interfaces.Services;
using GestaoPortfolio.Domain.Kafka;
using GestaoPortfolio.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GestaoPortifolio.API.Controllers
{
    public class OperacaoController : BaseController
    {
        private readonly IOperacaoFacade operacaoFacade;
        private readonly IOperacaoRepository operacaoRepository;
        private readonly IProdutorKafka produtorKafka;
        public OperacaoController(IProdutorKafka produtor, IOperacaoFacade operacaoFacade, IOperacaoRepository operacaoRepository, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            produtorKafka = produtor;
            this.operacaoFacade = operacaoFacade;
            this.operacaoRepository = operacaoRepository;
        }

        [HttpGet]
        [Route("listar")]
        public async Task<IActionResult> GetOperacao([FromQuery] Operacao operacao)
        {
            var resultado = await operacaoRepository.Listar(operacao);
            return Ok(resultado);
        }

        [HttpGet]
        [Route("extrato")]
        public async Task<IActionResult> GetExtrato([FromQuery] Operacao operacao)
        {
            var resultado = await operacaoRepository.ListarExtrato(operacao);
            return Ok(resultado);
        }

        [HttpPost]
        [Route("incluir")]
        public async Task<IActionResult> PostOperacao([FromBody] Operacao operacao)
        {
            var resultado = await operacaoFacade.Inserir(operacao);
            if(resultado == null)
            {
                return BadRequest("Quantidade inválida!");
            }else
            {
                return Ok(resultado);
            }

        }

        [HttpPut]
        [Route("alterar")]
        public async Task<IActionResult> PutOperacao([FromBody] Operacao operacao)
        {
            var resultado = await operacaoFacade.Alterar(operacao);
            return Ok(resultado);
        }

        [HttpPost]
        [Route("incluir/venda")]
        public async Task<IActionResult> PostOperacaoVenda([FromBody] Operacao operacao)
        {
            await produtorKafka.ProduzirMensagem("ORDEM_VENDA", userId?.ToString(), JsonConvert.SerializeObject(operacao));            
            return Ok();
        }


        [HttpPost]
        [Route("incluir/compra")]
        public async Task<IActionResult> PostOperacaoCompra([FromBody] Operacao operacao)
        {
            await produtorKafka.ProduzirMensagem("ORDEM_COMPRA", userId?.ToString(), JsonConvert.SerializeObject(operacao));
            return Ok();
        }
    }
}
