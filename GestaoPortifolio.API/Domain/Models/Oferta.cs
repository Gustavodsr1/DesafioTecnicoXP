using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GestaoPortfolio.Domain.Models
{
    public class Oferta : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("codigo_oferta")]
        public int CodigoOferta { get; set; }
        [Column("codigo_produto")]
        public int CodigoProduto { get; set; }
        [Column("nome_papel")]
        public string Papel { get; set; }
        [Column("quantidade_disponivel")]
        public int QuantidadeDisponivel { get; set; }
        [Column("quantidade_original")]
        public int QuantidadeOriginal { get; set; }
        [Column("preco_unitario")]
        public double PrecoUnitario { get; set; }
        [Column("data_vencimento")]
        public DateTime DataVencimento { get; set; }
        [Column("ativo")]
        public bool Ativo { get; set; }
        [Column("data_insercao")]
        public DateTime? DataInsercao { get; set; }
        [Column("data_ultima_atualizacao")]
        public DateTime? DataUltimaAtualizacao { get; set; }
        [Column("codigo_usuario_atualizacao")]
        public int? CodigoUsuarioAtualizacao { get; set; }
    }
}
