using GestaoPortfolio.Domain.Kafka;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoPortfolio.Application.Background
{
    public class OrdemVendaHostedService : BackgroundService
    {
        private readonly IOrdemVendaConsumidor ordemVendaConsumidor;

        public OrdemVendaHostedService(IOrdemVendaConsumidor ordemCompraConsumidor)
        {
            this.ordemVendaConsumidor = ordemCompraConsumidor;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Yield();
            await ordemVendaConsumidor.Consumir("ORDEM_VENDA", stoppingToken);
        }
    }
}
