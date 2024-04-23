using GestaoPortfolio.Domain.Interfaces;
using GestaoPortfolio.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoPortfolio.Application.Services
{
    public class ExcluirProdutoService : IExcluirProdutoService
    {
        private readonly IProdutoRepository produtoRepository;

        public ExcluirProdutoService(IProdutoRepository produtoRepository)
        {
            this.produtoRepository = produtoRepository;
        }

        public async Task Excluir(int id)
        {
            await produtoRepository.Delete(id);
        }
    }
}
