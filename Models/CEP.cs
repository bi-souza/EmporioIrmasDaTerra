using System.ComponentModel.DataAnnotations;

namespace EmporioIrmasDaTerra.Models
{
    public class CEP
    {
        // Chave Primária da tabela CEP, representada pelo próprio valor do CEP.
        [Key]
        public string CepValor { get; set; } = string.Empty;

        

        // Relacionamento N-para-1: Um CEP pertence a UMA Cidade.
        public int IdCidade { get; set; } // Chave Estrangeira.
        public Cidade Cidade { get; set; } = null!; // Propriedade de Navegação.

        // Relacionamento 1-para-N: Um CEP pode estar em N Endereços.
        public ICollection<Endereco> Enderecos { get; set; } = new List<Endereco>();
    }
}