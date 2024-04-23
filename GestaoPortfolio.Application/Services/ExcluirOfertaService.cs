using GestaoPortfolio.Domain.Interfaces;
using GestaoPortfolio.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoPortfolio.Application.Services
{
    public class ExcluirOfertaService : IExcluirOfertaService
    {
        private readonly IOfertaRepository ofertaRepository;

        public ExcluirOfertaService(IOfertaRepository ofertaRepository)
        {
            this.ofertaRepository = ofertaRepository;
        }

        public async Task Excluir(int id)
        {
            await ofertaRepository.Delete(id);
        }
    }
}
