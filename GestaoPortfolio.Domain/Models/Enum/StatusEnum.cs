using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoPortfolio.Domain.Models.Enum
{
    public class StatusEnum
    {
        public enum Status
        {
            Gravada,
            Cancelada,
            Erro,
            Vencida,
            Resgatada
        }
    }
}
