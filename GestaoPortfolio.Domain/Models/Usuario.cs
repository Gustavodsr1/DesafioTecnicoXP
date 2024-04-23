using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GestaoPortfolio.Domain.Models
{
    [Table("USUARIO")]
    public class Usuario : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("codigo_usuario")]
        public int CodigoUsuario { get; set; }
        [Column("nome")]
        public string Nome { get; set; }
        [Column("email")]
        public string Email { get; set; }
        [Column("tipo")]
        public int Tipo { get; set; }
        [Column("data_insercao")]
        public DateTime? DataInsercao { get; set; }
        
        /// <summary>
        /// Transitorio para testes
        /// </summary>
        [NotMapped]
        public string Senha { get; set; }
    }
}
