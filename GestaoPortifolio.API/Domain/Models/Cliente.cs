using GestaoPortfolio.Domain.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace GestaoPortfolio.Domain.Models
{
    [Table("CLIENTE")]
    public class Cliente : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_cliente")]
        public int Id { get; set; }
        [Column("nome_cliente")]
        public string Nome { get; set; }
        [Column("email")]
        public string Email { get; set; }
        [Column("numero_conta")]
        public string Conta { get; set; }
        [Column("numero_agencia")]
        public string Agencia { get; set; }
        [Column("tipo_pessoa")]
        public char TipoPessoa { get; set; }
        [Column("documento")]
        public string DocumentoIdentificacao { get; set; }
        [Column("ativo")]
        public bool Ativo { get; set; } 
    }   
}
