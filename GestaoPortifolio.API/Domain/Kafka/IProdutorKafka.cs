using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoPortfolio.Domain.Kafka
{
    public interface IProdutorKafka
    {
        Task ProduzirMensagem(string topico, string chave, string mensagemJson);
    }
}
