using GestaoPortfolio.Domain.Interfaces;
using GestaoPortfolio.Domain.Interfaces.Services;
using GestaoPortfolio.Domain.Models;

namespace GestaoPortfolio.Application.Services
{
    public class AlterarProdutoService : IAlterarProdutoService
    {
        private readonly IProdutoRepository produtoRepository;

        public AlterarProdutoService(IProdutoRepository produtoRepository)
        {
            this.produtoRepository = produtoRepository;
        }

        public async Task<Produto> Alterar(Produto produto)
        {
            return await produtoRepository.Update(produto);
        }
    }
}
