using GestaoPortfolio.Domain.Interfaces;
using GestaoPortfolio.Domain.Interfaces.Facades;
using GestaoPortfolio.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestaoPortifolio.API.Controllers
{
    public class ProdutoController : BaseController
    {
        private readonly IProdutoFacade produtoFacade;
        private readonly IProdutoRepository produtoRepository;

        public ProdutoController(IProdutoFacade produtoFacade, IProdutoRepository produtoRepository, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            this.produtoFacade = produtoFacade;
            this.produtoRepository = produtoRepository;
        }

        [HttpGet]
        [Route("listar")]
        public async Task<IActionResult> GetProduto([FromQuery] Produto produto)
        {
            var resultado = await produtoRepository.Listar(produto);
            return Ok(resultado);
        }

        [HttpPost]
        [Route("criar")]
        public async Task<IActionResult> PostProduto([FromBody] Produto produto)
        {
            produto.DataInsercao = DateTime.Now;
            produto.DataUltimaAtualizacao = DateTime.Now;
            var resultado = await produtoFacade.Inserir(produto);
            return Ok(resultado);
        }

        [HttpPut]
        [Route("alterar")]
        public async Task<IActionResult> PutProduto([FromBody] Produto produto)
        {
            produto.DataInsercao = DateTime.Now;
            produto.DataUltimaAtualizacao = DateTime.Now;
            var resultado = await produtoFacade.Alterar(produto);
            return Ok(resultado);
        }

    }
}
