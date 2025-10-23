using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmporioIrmasDaTerra.Models
{
    public class Endereco
    {
        [Key]
        // Chave Primária da tabela Endereco.
        public int IdEndereco { get; set; }
        public string Logradouro { get; set; } = string.Empty;
        public string Numero { get; set; } = string.Empty;


        // Relacionamento N-para-1: Um Endereço pertence a UM Cliente.
        public int IdCliente { get; set; } // Chave Estrangeira.
        public Cliente Cliente { get; set; } = null!; // Propriedade de Navegação.

        // Relacionamento N-para-1: Um Endereço pertence a UM CEP.
        public string CepValor { get; set; } = string.Empty; // Chave Estrangeira (usando o valor do CEP).
        
        [ForeignKey("CepValor")]
        public CEP CEP { get; set; } = null!; // Propriedade de Navegação.
    }
}