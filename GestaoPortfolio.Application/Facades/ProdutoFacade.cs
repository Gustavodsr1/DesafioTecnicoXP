using GestaoPortfolio.Domain.Interfaces.Facades;
using GestaoPortfolio.Domain.Interfaces.Services;
using GestaoPortfolio.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoPortfolio.Application.Facades
{
    public class ProdutoFacade : IProdutoFacade
    {
        private readonly IIncluirProdutoService incluirProdutoService;
        private readonly IAlterarProdutoService alterarProdutoService;
        private readonly IExcluirProdutoService excluirProdutoService;

        public ProdutoFacade(IIncluirProdutoService incluirProdutoService, IAlterarProdutoService alterarProdutoService, IExcluirProdutoService excluirProdutoService)
        {
            this.incluirProdutoService = incluirProdutoService;
            this.alterarProdutoService = alterarProdutoService;
            this.excluirProdutoService = excluirProdutoService;
        }

        public async Task<Produto> Alterar(Produto produto)
        {
            return await alterarProdutoService.Alterar(produto);
        }

        public async Task Excluir(int id)
        {
            await excluirProdutoService.Excluir(id);
        }

        public async Task<Produto> Inserir(Produto produto)
        {
            return await incluirProdutoService.Incluir(produto);
        }
    }
}
