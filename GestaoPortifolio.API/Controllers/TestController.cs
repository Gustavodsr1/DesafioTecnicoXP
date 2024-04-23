using GestaoPortfolio.Domain.Interfaces;
using GestaoPortfolio.Domain.Interfaces.Facades;
using GestaoPortfolio.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace GestaoPortifolio.API.Controllers
{
    public class TestController : BaseController
    {
        private readonly IProdutoFacade produtoFacade;
        private readonly IProdutoRepository produtoRepository;

        public TestController(IProdutoFacade produtoFacade, IProdutoRepository produtoRepository)
        {
            this.produtoFacade = produtoFacade;
            this.produtoRepository = produtoRepository;
        }

        [HttpGet]
        [Route("teste")]
        public async Task<IActionResult> Get()
        {
            return await Task.FromResult(Ok("Olá mundo"));
        }

        [HttpGet]
        [Route("produto")]
        public async Task<IActionResult> GetProduto([FromQuery] Produto produto)
        {
            var resultado = await produtoRepository.Listar(produto);
            return Ok(resultado);
        }

        [HttpPost]
        [Route("produto")]
        public async Task<IActionResult> PostProduto([FromBody] Produto produto)
        {
            var resultado = await produtoFacade.Inserir(produto);
            return Ok(resultado);
        }

        [HttpPut]
        [Route("produto")]
        public async Task<IActionResult> PutProduto([FromBody] Produto produto)
        {
            var resultado = await produtoFacade.Alterar(produto);
            return Ok(resultado);
        }

        [HttpDelete]
        [Route("produto/{id}")]
        public async Task<IActionResult> DeleteProduto([FromRoute] int id)
        {
            await produtoFacade.Excluir(id);
            return Ok();
        }
    }
}
