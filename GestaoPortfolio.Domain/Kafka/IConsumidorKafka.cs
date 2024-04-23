using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoPortfolio.Domain.Kafka
{
    public interface IConsumidorKafka
    {
        Task Consumir(string topico, CancellationToken cancellationToken = default);
    }
}
