using GestaoPortfolio.Domain.Kafka;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoPortfolio.Application.Background
{
    public class OrdemCompraHostedService : BackgroundService
    {
        private readonly IOrdemCompraConsumidor ordemCompraConsumidor;

        public OrdemCompraHostedService(IOrdemCompraConsumidor ordemCompraConsumidor)
        {
            this.ordemCompraConsumidor = ordemCompraConsumidor;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Yield();
            await ordemCompraConsumidor.Consumir("ORDEM_COMPRA", stoppingToken);
        }
    }
}
