using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GestaoPortfolio.Domain.Models.Enum.EventoEnum;

namespace GestaoPortfolio.Domain.Models
{
    public class ExtratoProduto : BaseEntity
    {
        public int IdOperacao { get; set; }
        public int CodigoProduto { get; set; }
        public int CodigoOferta { get; set; }
        public int QuantidadeMovimentada { get; set; }
        public double ValorOperacao { get; set; }
        public Evento TipoEvento { get; set; }
        public DateTime? DataMovimentacao { get; set; }
        public int QuantidadeDisponivelEstoque { get; set; }
        public int IdCliente { get; set; }
    }
}
