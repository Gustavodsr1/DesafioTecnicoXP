using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoPortfolio.Domain.Models
{
    [Table("JOBS")]
    public class Job : BaseEntity
    {
        public enum kdTipoInvervalo
        {
            Diario,
            Continuo
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("codigo_job")]
        public int CodigoJob { get; set; }
        [Column("sigla")]
        public string Sigla { get; set; }
        [Column("descricao")]
        public string Descricao { get; set; }
        [Column("inicio")]
        public int Inicio { get; set; }
        [Column("intervalo_minutos")]
        public int IntervaloMinutos { get; set; }
        [Column("ultima_execucao")]
        public DateTime? UltimaExecucao { get; set; }
        [Column("proxima_execucao")]
        public DateTime? ProximaExecucao { get; set; }
        [Column("tipo_intervalo")]
        public int TipoIntervalo { get; set; }

        public void CalcularProximaExecucao()
        {
            kdTipoInvervalo tipoIntervalo = (kdTipoInvervalo)TipoIntervalo;

            switch (tipoIntervalo)
            {
                case kdTipoInvervalo.Diario:
                    if (ProximaExecucao == null)
                        ProximaExecucao = DateTime.Today.AddHours(Inicio);
                    ProximaExecucao = ProximaExecucao.Value.AddDays(1);
                    break;
                case kdTipoInvervalo.Continuo:
                    ProximaExecucao = DateTime.Now.AddMinutes(IntervaloMinutos);
                    break;
            }
        }
    }
}
