using GestaoPortfolio.Application.Facades;
using GestaoPortfolio.Domain.Interfaces.Facades;
using GestaoPortfolio.Domain.Interfaces;
using GestaoPortfolio.Domain.Kafka;
using GestaoPortfolio.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using GestaoPortfolio.Infra.Repository;

namespace GestaoPortifolio.API.Controllers
{
    [Authorize]
    public class OfertaController : BaseController
    {
        private readonly IOfertaFacade ofertaFacade;
        private readonly IOfertaRepository ofertaRepository;

        public OfertaController(IOfertaFacade ofertaFacade, IOfertaRepository ofertaRepository, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            this.ofertaFacade = ofertaFacade;
            this.ofertaRepository = ofertaRepository;
        }

        [HttpPost]
        [Route("criar")]
        public async Task<IActionResult> PostOferta([FromBody] Oferta oferta)
        {
            oferta.DataInsercao = DateTime.Now;
            oferta.DataUltimaAtualizacao = DateTime.Now;
            var resultado = await ofertaFacade.Inserir(oferta);
            return Ok(resultado);
        }

        [HttpPut]
        [Route("alterar")]
        public async Task<IActionResult> PutOferta([FromBody] Oferta oferta)
        {
            oferta.DataUltimaAtualizacao = DateTime.Now;
            var resultado = await ofertaFacade.Alterar(oferta);
            return Ok(resultado);
        }

        [HttpGet]
        [Route("listar")]
        public async Task<IActionResult> GetOferta([FromQuery] Oferta oferta)
        {
            var resultado = await ofertaRepository.Listar(oferta);
            return Ok(resultado);
        }
    }
}
