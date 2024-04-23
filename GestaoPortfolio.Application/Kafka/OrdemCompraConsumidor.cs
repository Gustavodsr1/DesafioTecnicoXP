using GestaoPortfolio.Domain.Interfaces.Facades;
using GestaoPortfolio.Domain.Interfaces.Services;
using GestaoPortfolio.Domain.Kafka;
using GestaoPortfolio.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Diagnostics;
using static GestaoPortfolio.Domain.Models.Enum.EventoEnum;
using static GestaoPortfolio.Domain.Models.Enum.StatusEnum;

namespace GestaoPortfolio.Application.Kafka
{
    public class OrdemCompraConsumidor : ConsumidorKafka, IOrdemCompraConsumidor
    {
        private readonly IServiceScopeFactory serviceScopeFactory;

        public OrdemCompraConsumidor(IConfiguration configuration, IServiceScopeFactory serviceScopeFactory) 
            : base(configuration)
        {
            this.serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task Processar(string mensagem)
        {
            Debug.WriteLine(mensagem);

            Operacao operacao = JsonConvert.DeserializeObject<Operacao>(mensagem);

            using (var scope = serviceScopeFactory.CreateScope())
            {
                IAtualizarCarteiraEstoqueService atualizarCarteiraEstoque = scope.ServiceProvider.GetRequiredService<IAtualizarCarteiraEstoqueService>();

                operacao.Evento = Evento.Compra;
                operacao.DataOperacao = DateTime.Now;
                operacao.Status = Status.Gravada;
                await atualizarCarteiraEstoque.TratarOperacao(operacao);
            }
        }
    }
}
