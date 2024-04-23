using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GestaoPortfolio.Domain.Models
{
    [Table("PRODUTO")]
    public class Produto : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("codigo_produto")]
        public int Codigo { get; set; }
        [Column("nome")]
        public string Nome { get; set; }
        [Column("descricao")]
        public string Descricao { get; set; }
        [Column("ativo")]
        public bool ativo { get; set; }
        [Column("data_insercao")]
        public DateTime? DataInsercao { get; set; }
        [Column("data_ultima_atualizacao")]
        public DateTime? DataUltimaAtualizacao { get; set; }
        [Column("codigo_usuario_atualizacao")]
        public int? CodigoUsuarioAtualizacao { get; set; }
    }
}
